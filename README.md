# Tutorial.EntityFrameworkUpdate 
A sample tutorial to show the how EF generates SQL UPDATE statements.
EF can generate either a *Full* or *Partial* SQL UPDATE statement.
The trick is understanding how to do one or the other.

There are additional items that are included that one might be interested in.  
1.  Example of the [Clean Architecture](https://levelup.gitconnected.com/clean-architecture-86c4f03e4771).
2.  Example of the [Decorator Pattern](https://refactoring.guru/design-patterns/decorator/csharp/example), including setup via Dependency Injection.  
3.  Two examples of using [HTTP Patch](https://en.wikipedia.org/wiki/PATCH_(HTTP)) method.  
    -  Updating individual object fields.
    -  Updating a list of objects, where the objects are either added or deleted
4.  This uses [SQLite](https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/?tabs=net-cli) as an in-memory database;

### Sample application setup
1.  A simple inventory application that consists of the following: 
    -  Categories
        -  Each Category has zero or more Products.
    -  Products
        -  Member of a single Category.
        -  Can have zero or more Tags (Product Tags).
    -  Product Tags
        -  Member of a single Product.
        -  Cannot have more than one tag with the same name to the same Product.
2.  No UI, just a backend CRUD based [REST API](https://restfulapi.net/) application.
3.  Uses SQLite as an in-memory database.
    -  Database is recreated everytime the application starts up.
4.  Uses the following patterns:
    -  [Mediator](https://refactoring.guru/design-patterns/mediator)
    -  Command Query Responsibilty Segregation ([CQRS](https://learn.microsoft.com/en-us/azure/architecture/patterns/cqrs))
    -  [Decorator](https://refactoring.guru/design-patterns/decorator/csharp/example)
    -  [Repository](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design)
    -  [Inversion of Control](https://en.wikipedia.org/wiki/Inversion_of_control) using [Dependency Injection](https://en.wikipedia.org/wiki/Dependency_injection)


## The *Full* UPDATE statement
This is where EF generates a SQL statement with **all** fields being updated, whether or not the field's value has been modified.
#### Reason(s)
1.  Entity is not being [tracked](https://learn.microsoft.com/en-us/ef/core/querying/tracking).
    -  The entity was not previously queried from the database.
    -  The entity was queried from a database that is not the same as the one being updated.  This occurs where the entity is read from a replicated database.
    -  The entity was queried from a database with the AsNoTracking attribute/method.

#### Nuance(s)
1.  The entire entity object must be passed to the database context's Update method.
    ```    
    context.Update(productEntity);
    await context.SaveChangesAsync();
    ```
#### Performance Impact
1.  All field/property's values must exist, modified or not (-).
    -  If a property value is not included, then based upon the data type of the property, it's default value will be used.
2.  The entire entity object will need to originate from the source (-).
    -  If this comes from an external application, ie UI, the payload may be large.
3.  The underlying database may perform additional processing on fields, even if those fields were not modified (-).


## The *Partial* UPDATE statement
This is where EF generates a SQL statement where only the fields who's values have been modified.  
There are 3 ways to ensure that EF generates a *Partial* UPDATE statement

### 1 - Using EF 's built-in change detection
EF have the ability to determine which fields were modified to assist in generating the SQL statement.
#### Reason(s)
1.  Entity is being [tracked](https://learn.microsoft.com/en-us/ef/core/querying/tracking).
    -  The entity was previously read in from the same database context as will be written back to.
2.  The tracked entity must have one or more fields modified.
3.  Do not use the .Update method, instead just call the .SaveChanges method.

#### Nuance(s)
1.  The entire entity must be read from the same database context that will be written back to.
2.  If the original tracked entity is not directly updated, then a mapping will need to be done, such that, the target is the tracked entity.
3.  If entity is read from a different database context than will be written to.  Will need to re-read entity back into memory
    ```    
    // Read entity from database context that will be written to
    var trackedEntity = await context.Products
                                     .Where(x => x.id == id)
                                     .FirstOrDefaultAsync();
    
    // Update tracked entity fields
    trackedEntity.Price = price;
    trackedEntity.Quantity = quantity;

    // Use the tracked entity to have EF detect changes
    context.Update(trackedEntity);
    await context.SaveChangesAsync();
    ```
#### Performance Impact
1.  Entity must be read in from the database at least once (-).
    -  EF requires the entity to be tracked.
2.  Entity must be tracked (-).
    -  A tracked entity is stored in memory using an in-memory cache.
    -  Depending the number of entities or an entity's payload size,  might consume a large amount of memory.
3.  May have unneccessary processing when mapping (-).
    -  If the source entity contains many fields, but only 3 are modified.


### 2 - Using EF's ExecuteUpdate method
Starting with EF Core 7, two new methods were introduced:
-  ExecuteUpdate - Allows one ore more entities to be updated without using the context Update() method and allows developer to explicity set the Where clause.
-  ExecuteDelete - Allows one ore more entities to be deleted without using the context Delete() method and allows developer to explicity set the Where clause.
#### Reason(s)
1.  Gives developer more control.
    -  Controls the Where clause.
    -  Not restricted to Primary Key of object.
2.  Database entities do not need be tracked or read in from the database prior.

#### Nuance(s)
1.  If there are a variable number of fields that can be updated, then constructing the Linq statement might be troublesome or counter productive.
2.  Will need to be modified if new fields/properties are added to the database entity.
3.  Developer will need to construct the WHERE clause.
    ```    
    DateTime archiveDate = DateTime.Today.AddDays(-30);

    // This will update zero or more entities ArchiveFlag
    await context.Products
                 .Where(x => x.DateEntered <= archiveDate)
                 .ExecuteUpdateAsync(x => 
                        x.SetProperty(p => p.ArchiveFlag, true));
    ```

#### Performance Impact
1.  Only one SQL UPDATE Statement is generated that can affect multiple entities (+).
2.  Large number of entities may changed (-).
    -  Causing extended table locking.
    -  Causing transaction log to fill up.
3.  Unintentional entities may be changed (-).


### 3 - Using a separate DB Context for Updates
This is a specialized scenario where one can have the following benefits
-  Entity does not need to be tracked.
-  Clearly define which fields can be updated.
-  Allow EF to fully generate the SQL Statement.
    -  Not using ExecuteUpdate
-  Works with all versions of EF.

#### Reason(s)
1.  Uses a separate database context.
    -  EF strictly enforces a one to one relationship between a database context and an entity contract (ie class).
2.  Uses a partial contract of the original entity.
    -  Defines only the fields that will be updated.
    -  Since EF only knows of the partial contract, it will generate a **Full** UPDATE statement of that partial contract, even though the database table contains additional fields.

#### Nuance(s)
1.  Must create a separate class (ie partial contract).
2.  Must create a separate database context.

#### Performance Impact
1.  Like the **Full** UPDATE scenario, all the fields must be supplied.
    -  Fields will be clearly defined and notably less than the original contract (+).
    -  All fields' values still must supplied, even if only one is being modified (-).
2.  Does not require the entity to be tracked (+).
    -  Not stored in memory.
    -  Does not require a prior separate querying of the entity.
