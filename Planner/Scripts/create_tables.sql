CREATE TABLE hiker
(
	id INT PRIMARY KEY IDENTITY (1, 1),
	user_name VARCHAR (50) NOT NULL,
	password VARCHAR(500) NOT NULL,
	first_name VARCHAR (50) NOT NULL,
	last_name VARCHAR (50) NOT NULL,
	phone VARCHAR (50) NOT NULL,
	city VARCHAR (50) NOT NULL,
	emergency_contact_name VARCHAR (50) NOT NULL,
	emergency_contact_phone VARCHAR (50) NOT NULL,
	fun_scale INT NOT NULL,
	has_car BIT NOT NULL,
	car_brand VARCHAR(50),
	car_model VARCHAR(50),
	awd BIT,
	snow_friendly BIT,
	high_clearance BIT,
	spaces INT,
	preference INT, -- make enum in the code to parse that (Alone, Driver, Rider, etc.)
);

CREATE TABLE hiker_gear
(
	id INT PRIMARY KEY IDENTITY (1, 1),
	hiker_id INT NOT NULL,
	item VARCHAR(50) NOT NULL,
	brand VARCHAR(50) NOT NULL,
	model VARCHAR(50) NOT NULL,
	intended_use INT NOT NULL, -- make an enum in code to parse int value
	group_use BIT NOT NULL,
	weight INT NOT NULL,
	number_of_users INT NOT NULL,
	specs VARCHAR(50) NOT NULL,
	details VARCHAR(MAX) NULL, -- This is to cover Roberto idea of more details of the gear; ideally should be expanded into either separate table or extend that table to cover possibilities
	FOREIGN KEY (hiker_id) REFERENCES hiker (id)
);

-- We decided we don't need separate table for that now
-- I am keeping it in the schema for now if we decide to support more that 1 car per user
CREATE TABLE carpool
(
	id INT PRIMARY KEY IDENTITY (1, 1),
	hiker_id INT NOT NULL,
	car_brand VARCHAR(50) NOT NULL,
	car_model VARCHAR(50) NOT NULL,
	spaces INT NOT NULL,
	preference INT NOT NULL, -- make enum in the code to parse that (Alone, Driver, Rider, etc.)
	awd BIT NOT NULL,
	snow_friendly BIT NOT NULL,
	high_clearance BIT NOT NULL,
	FOREIGN KEY (hiker_id) REFERENCES hiker (id)
);

CREATE TABLE permit
(
	id INT PRIMARY KEY IDENTITY (1, 1),
	hiker_id INT NOT NULL,
	permit_type VARCHAR(30) NOT NULL, -- have enum in code for that
	expiration_date DATE NOT NULL,
	FOREIGN KEY (hiker_id) REFERENCES hiker (id)
);

CREATE TABLE group_gear -- table to store group gear requirements by leader
(
	id INT PRIMARY KEY IDENTITY (1, 1),
	trip_id INT NOT NULL,
	item VARCHAR(50) NOT NULL,
	number INT NOT NULL,
	FOREIGN KEY (trip_id) REFERENCES trip (id)
);

CREATE TABLE peak
(
	id INT PRIMARY KEY IDENTITY (1, 1),
	name VARCHAR (50) NOT NULL,
	routes VARCHAR (1000)
);

CREATE TABLE trip
(
    id INT PRIMARY KEY IDENTITY (1, 1),
    name VARCHAR (50) NOT NULL,
    days INT NOT NULL,
    start_date DATETIME NOT NULL,
	peak_id INT NOT NULL,
	owner_id INT NOT NULL,
	group_size INT NOT NULL CHECK (group_size > 0),
	FOREIGN KEY (peak_id) REFERENCES peak (id),
	FOREIGN KEY (owner_id) REFERENCES hiker (id),
	members VARCHAR (100) NOT NULL, -- this field should be removed, and members should come from lookup of TripHiker table
	location VARCHAR(100) DEFAULT(''),
	hasSnow BIT DEFAULT(0),
	isBumpyRoad BIT DEFAULT(0),
	needHighClearanceVehicle  BIT DEFAULT(0),
	elevationGain INT DEFAULT(0),
	totalDistance Float default(0)
);

CREATE TABLE hikerTrip
(
    id INT PRIMARY KEY IDENTITY (1, 1),
	hiker_id INT NOT NULL,
	trip_id INT NOT NULL,
	FOREIGN KEY (hiker_id) REFERENCES hiker (id),
	FOREIGN KEY (trip_id) REFERENCES trip (id)
);

CREATE TABLE rangerStation
(
    id INT PRIMARY KEY IDENTITY (1, 1),
	name VARCHAR(200) NOT NULL,
	phone VARCHAR(50) NOT NULL,
);
