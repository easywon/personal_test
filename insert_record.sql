CREATE PROCEDURE InsertRecord
	@Name nvarchar(50)
AS
INSERT INTO Source(Name) VALUES (@Name);

EXEC InsertRecord @Name = 'Ben';
