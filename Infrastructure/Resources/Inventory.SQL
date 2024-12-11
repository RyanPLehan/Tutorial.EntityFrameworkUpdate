CREATE TABLE Category
(      
    CategoryID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    CategoryName TEXT NOT NULL,
    Description TEXT NOT NULL
);

CREATE TABLE Product(
    ProductID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    ProductName TEXT NOT NULL,
    Description TEXT NOT NULL,
    Price NUMERIC  NOT NULL DEFAULT 0,
	Quantity INTEGER  NOT NULL DEFAULT 0,
    CategoryID INTEGER NOT NULL,
	FOREIGN KEY (CategoryID) REFERENCES Category (CategoryID)
);

CREATE TABLE ProductTag(
    ProductTagID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    TagName TEXT NOT NULL,
	TagValue TEXT NULL,
    ProductID INTEGER NOT NULL,
	FOREIGN KEY (ProductID) REFERENCES Product (ProductID)
);

-- Category
INSERT INTO Category VALUES (1, 'Mobile Devices', 'Devices that go where you want to go');
INSERT INTO Category VALUES (2, 'Wearables', 'Accessorize those mobile devices');
INSERT INTO Category VALUES (3, 'Entertainment', 'Be the talk of the town with our TVs, Set Top Boxes, and Surround Sound Systems');
INSERT INTO Category VALUES (4, 'Productivity', 'Working from home or office, our computers, laptops and tablets will help you get it done');

-- Product
INSERT INTO Product VALUES (1, 'iPhone 12', 'Apple phone', 999.99, 3, 1);
INSERT INTO Product VALUES (2, 'iPhone 13', 'Apple phone', 1100.00, 20, 1);
INSERT INTO Product VALUES (3, 'iPhone 14', 'Apple phone', 2500.00, 40, 1);
INSERT INTO Product VALUES (4, 'Galaxy S 17', 'Samsung phone', 799.99, 2, 1);
INSERT INTO Product VALUES (5, 'Galaxy S 18', 'Samsung phone', 849.99, 7, 1);
INSERT INTO Product VALUES (6, 'Galaxy S 19', 'Samsung phone', 899.99, 0, 1);
INSERT INTO Product VALUES (7, 'Pixel 9', 'Google phone', 499.99, 4, 1);

INSERT INTO Product VALUES (8, 'AirBuds', 'Apple Ear Phones', 99.99, 73, 2);
INSERT INTO Product VALUES (9, 'iWatch', 'Apple Watch', 299.99, 17, 2);

INSERT INTO Product VALUES (10, 'Roku TV', 'LED TV', 199.99, 100, 3);
INSERT INTO Product VALUES (11, 'Samsumg TV', 'LED TV', 599.99, 100, 3);
INSERT INTO Product VALUES (12, 'Sony TV', 'Plasma TV', 1599.99, 1, 3);

INSERT INTO Product VALUES (13, 'Dell Latitude', 'Laptop', 1599.99, 9, 4);
INSERT INTO Product VALUES (14, 'Dell Plex', 'Desktop', 1299.99, 15, 4);
INSERT INTO Product VALUES (15, 'Surface Pro', 'MS Tablet', 699.99, 12, 4);
INSERT INTO Product VALUES (16, 'iPad Pro', 'Apple Tablet', 749.99, 19, 4);

-- Product Tags
INSERT INTO ProductTag VALUES (1, 'Operating System', 'iOS', 1);
INSERT INTO ProductTag VALUES (2, 'Version', '12', 1);
INSERT INTO ProductTag VALUES (3, 'Speed', '4G', 1);

INSERT INTO ProductTag VALUES (4, 'Operating System', 'iOS', 2);
INSERT INTO ProductTag VALUES (5, 'Version', '13', 2);
INSERT INTO ProductTag VALUES (6, 'Speed', '4G', 2);

INSERT INTO ProductTag VALUES (7, 'Operating System', 'iOS', 3);
INSERT INTO ProductTag VALUES (8, 'Version', '14', 3);
INSERT INTO ProductTag VALUES (9, 'Speed', '5G', 3);

INSERT INTO ProductTag VALUES (10, 'Operating System', 'Android', 4);
INSERT INTO ProductTag VALUES (11, 'Version', '17', 4);
INSERT INTO ProductTag VALUES (12, 'Speed', '4G', 4);

INSERT INTO ProductTag VALUES (13, 'Operating System', 'Android', 5);
INSERT INTO ProductTag VALUES (14, 'Version', '18', 5);
INSERT INTO ProductTag VALUES (15, 'Speed', '4G', 5);

INSERT INTO ProductTag VALUES (16, 'Operating System', 'Android', 6);
INSERT INTO ProductTag VALUES (17, 'Version', '19', 6);
INSERT INTO ProductTag VALUES (18, 'Speed', '4G', 6);

INSERT INTO ProductTag VALUES (19, 'Operating System', 'Android', 7);
INSERT INTO ProductTag VALUES (20, 'Version', '9', 7);
INSERT INTO ProductTag VALUES (21, 'Speed', '5G', 7);

INSERT INTO ProductTag VALUES (22, 'Connectivity', 'Blue Tooth', 8);
INSERT INTO ProductTag VALUES (23, 'Connectivity', 'Blue Tooth', 9);

INSERT INTO ProductTag VALUES (24, 'Resolution', '1080', 10);
INSERT INTO ProductTag VALUES (25, 'Resolution', '4K', 11);
INSERT INTO ProductTag VALUES (26, 'Resolution', '720', 12);

INSERT INTO ProductTag VALUES (27, 'CPU', 'Intel i5', 13);
INSERT INTO ProductTag VALUES (28, 'CPU Generation', '12', 13);
INSERT INTO ProductTag VALUES (29, 'Chipset', 'B790', 13);
INSERT INTO ProductTag VALUES (30, 'Memory', '16GB', 13);
INSERT INTO ProductTag VALUES (31, 'Screen Size', '15', 13);
INSERT INTO ProductTag VALUES (32, 'Storage Size', '256GB', 13);
INSERT INTO ProductTag VALUES (33, 'Storage Type', 'SSD', 13);

INSERT INTO ProductTag VALUES (34, 'CPU', 'Intel i9', 14);
INSERT INTO ProductTag VALUES (35, 'CPU Generation', '14', 14);
INSERT INTO ProductTag VALUES (36, 'Chipset', 'Z790', 14);
INSERT INTO ProductTag VALUES (37, 'Memory', '32GB', 14);
INSERT INTO ProductTag VALUES (38, 'Storage Size', '2TB', 14);
INSERT INTO ProductTag VALUES (39, 'Storage Type', 'HDD', 14);

INSERT INTO ProductTag VALUES (40, 'Memory', '16GB', 15);
INSERT INTO ProductTag VALUES (41, 'Screen Size', '12', 15);

INSERT INTO ProductTag VALUES (42, 'Memory', '16GB', 16);
INSERT INTO ProductTag VALUES (43, 'Screen Size', '14', 16);
