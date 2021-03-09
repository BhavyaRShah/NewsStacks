create table tag(
	tagid int primary key identity,
	articleid int not null,
	tagname varchar(20),
	lastmodifieddate datetime not null,
	isdeleted bit not null,
	constraint fk_tag_articleid foreign key (articleid) references article(articleid))