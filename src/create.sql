drop table if exists user_ cascade;
create table user_
(
login text primary key,
password_ text,
name_ text,
surname text
);

drop table if exists company cascade;
create table company
(
companyid serial primary key,
title text,
foundationyear int
);

drop table if exists department cascade;
create table department
(
departmentid serial primary key,
title text,
company int not null,
foundationyear int,
activityfield text,

foreign key (company) references company(companyid) on delete cascade
);

drop table if exists employee cascade;
create table employee
(
employeeid serial primary key,
user_ text not null,
company int not null,
department int,
permission_ int not null,

foreign key (user_) references user_(login) on delete cascade,
foreign key (company) references company(companyid) on delete cascade,
foreign key (department) references department(departmentid) on delete cascade
);

drop table if exists objective cascade;
create table objective
(
objectiveid serial primary key,
parentobjective int,
title text,
company int not null,
department int,
termbegin date,
termend date,
estimatedtime interval,

foreign key (parentobjective) references objective(objectiveid) on delete cascade,
foreign key (company) references company(companyid) on delete cascade,
foreign key (department) references department(departmentid) on delete cascade
);

drop table if exists responsibility cascade;
create table responsibility
(
responsibilityid serial primary key,
employee int,
objective int,
timespent interval,

foreign key (employee) references employee(employeeid) on delete cascade,
foreign key (objective) references objective(objectiveid) on delete cascade
);

create view employeeview as
select e.employeeid employeeid, u.login login, u.name_ name_, u.surname surname, e.department department, e.permission_ permission_
from employee e join user_ u on e.user_ = u.login;
