SET IDENTITY_INSERT peak ON;

INSERT INTO peak
    (id, name, routes, trailhead_latitude, trailhead_longtitude)
VALUES
    (1, 'Mount Si', 'New trail;Old trail', 47.488225, -121.723185),
    (2, 'Poo poo point', 'Chirico;High school', 47.500155, -122.022027),
	(3, 'Mount Rainier', 'Disappointment Cleaver;Emmon-Winthrop', 46.786103, -121.736466)

SET IDENTITY_INSERT peak OFF;
