SET IDENTITY_INSERT hiker ON;

INSERT INTO hiker
    (id, first_name, last_name, phone, city, awd, emergency_contact_name, emergency_contact_phone, fun_scale)
VALUES
    (1, 'alex', 'password', 'Alex', 'Honnold', '100-000-0000', 'Seattle', 0, 'Sanni McCandless Honnold', '200-000-0000', 2),
    (2, 'jimmy', 'password', 'Jimmy', 'Chin', '300-000-0000', 'Redmond', 1, 'Elizabeth Chai Vasarhelyi', '400-000-0000', 1),
	(3, 'sacha', 'password', 'Sasha', 'DiGiulian', '500-000-0000', 'Everett', 1, 'Erik Osterholm', '600-000-0000', 3)

SET IDENTITY_INSERT hiker OFF;
