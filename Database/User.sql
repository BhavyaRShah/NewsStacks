
create table userdetail(
		userdetailid INT NOT NULL IDENTITY primary key,
		firstname varchar(30),
		lastname varchar(30) not null,
		username varchar(30) not null,
		userpassword varchar(100) not null,
		timezoneid int not null,
		iswriter bit not null,
		ispublisher bit not null,
		lastmodifieddate datetime not null,
		isdeleted bit not null,
		constraint fk_userdetail_timezoneid foreign key (timezoneid) references timezone(timezoneid))