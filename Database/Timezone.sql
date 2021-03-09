create table timezone(
	timezoneid int not null identity primary key,
	name varchar(50),
	offset varchar(15))