CREATE TABLE hiker
(
	id INT PRIMARY KEY IDENTITY (1, 1),
	first_name VARCHAR (50) NOT NULL,
	last_name VARCHAR (50) NOT NULL,
	phone VARCHAR (50) NOT NULL,
	city VARCHAR (50) NOT NULL,
	awd BIT NOT NULL,
	emergency_contact_name VARCHAR (50) NOT NULL,
	emergency_contact_phone VARCHAR (50) NOT NULL,
	fun_scale INT NOT NULL
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
	FOREIGN KEY (owner_id) REFERENCES hiker (id)
);
