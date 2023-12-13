create user notauth_role with encrypted password 'notauth';

grant select, insert on table user_ to notauth_role;

create user employee_role with encrypted password 'employee';

grant all on table user_ to employee_role;
grant select, insert on table company to employee_role;
grant select on table department to employee_role;
grant select, insert on table employee to employee_role;
grant select on table objective to employee_role;

create user founder_role with encrypted password 'founder';

grant all on table user_ to founder_role;
grant all on table company to founder_role;
grant all on table department to founder_role;
grant all on table employee to founder_role;
grant all on table objective to founder_role;
grant all on table responsibility to founder_role;

create user responsible_role with encrypted password 'responsible';

grant all on table user_ to responsible_role;
grant select, insert on table company to responsible_role;
grant select on table department to responsible_role;
grant select, insert on table employee to responsible_role;
grant all on table objective to responsible_role;
grant all on table responsibility to responsible_role;
grant all on table employeeview to responsible_role;

create user manager_role with encrypted password 'manager';

grant all on table user_ to manager_role;
grant select, insert on table company to manager_role;
grant all on table department to manager_role;
grant select, insert on table employee to manager_role;
grant all on table objective to manager_role;
grant all on table responsibility to manager_role;

create user hr_role with encrypted password 'hr';

grant all on table user_ to hr_role;
grant select, insert on table company to hr_role;
grant select on table department to hr_role;
grant all on table employee to hr_role;
grant select on table objective to hr_role;
grant select on table responsibility to hr_role;

create user user_role with encrypted password 'user';

grant all on table user_ to user_role;
grant select, insert on table company to user_role;
grant select on table department to user_role;
grant select, insert on table employee to user_role;
