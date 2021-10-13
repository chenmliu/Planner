SET IDENTITY_INSERT trip ON;

INSERT INTO trip
    (id, name, days, start_date, peak_id, owner_id, group_size)
VALUES
    (1, 'New Year Resolution', 1, '2022-01-01', 1, 1, 3, '1;2'),
	(2, 'Taco Tuesday', 2, '2021-05-25', 2, 2, 5, '1;2;3')

SET IDENTITY_INSERT trip OFF;
