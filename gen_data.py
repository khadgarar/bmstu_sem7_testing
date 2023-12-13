import psycopg2
from psycopg2 import Error
from psycopg2.extensions import ISOLATION_LEVEL_AUTOCOMMIT
from faker import Faker
from random import randint, choice
from uuid import uuid4
import string

RECORDS = 1000

logins = []

def randomword(length):
    letters = string.ascii_lowercase
    return ''.join(choice(letters) for i in range(length))

def generate_users(connection):
    fake = Faker()

    for i in range(RECORDS):
        login = fake.simple_profile()['username']
        if login in logins:
            login = login + "1"
        logins.append(login)
        
        password_ = randomword(randint(4, 45))
        name_ = fake.first_name()
        surname = fake.last_name()
        sql_insert = f"""INSERT INTO user_ (login, password_, name_, surname) 
                         VALUES (\'{login}\', \'{password_}\', \'{name_}\', \'{surname}\');"""
        cursor.execute(sql_insert)
        connection.commit()

def generate_companies(connection):
    fake = Faker()

    for i in range(RECORDS):
        companyid = i + 1
        title = fake.company()
        foundationyear = randint(1750, 2022)
        sql_insert = f"""INSERT INTO company (companyid, title, foundationyear) 
                         VALUES ({companyid}, \'{title}\', {foundationyear});"""
        cursor.execute(sql_insert)
        connection.commit()

departments_company = []
        
def generate_departments(connection):
    fake = Faker()

    for i in range(RECORDS):
        departmentid = i + 1
        title = fake.company()
        
        company = randint(1, RECORDS)
        departments_company.append(company)
        
        foundationyear = randint(1750, 2022)
        activityfield = fake.sentence(nb_words=10)
        sql_insert = f"""INSERT INTO department (departmentid, title, company, foundationyear, activityfield) 
                         VALUES ({departmentid}, \'{title}\', {company}, {foundationyear}, \'{activityfield}\');"""
        cursor.execute(sql_insert)
        connection.commit()
    
workplace_emp = [0]

def generate_employees(connection):
    fake = Faker()

    for i in range(RECORDS):
        employeeid = i + 1
        user_ = choice(logins)
        
        department = randint(0, RECORDS)
        company = 1
        if (department == 0):
            department = "null"
            company = randint(1, RECORDS)
            workplace_emp.append(company)
        else:
            company = departments_company[department - 1]
            workplace_emp.append(1000 + department)
            
        permission_ = randint(0, 4)
        sql_insert = f"""INSERT INTO employee (employeeid, user_, company, department, permission_) 
                         VALUES ({employeeid}, \'{user_}\', {company}, {department}, {permission_});"""
        cursor.execute(sql_insert)
        connection.commit()

def randominterval():
    days = randint(0, 50)
    if days != 0:
        return str(days) + " days " + str(randint(0, 23)) + ":" + str(randint(0, 59)) + ":" + str(randint(0, 59))
    else:
        return str(randint(0, 23)) + ":" + str(randint(0, 59)) + ":" + str(randint(0, 59))

company_obj = []
department_obj = []

def generate_objectives(connection):
    fake = Faker()
    
    for i in range(RECORDS + 1):
        company_obj.append([])
        department_obj.append([])

    for i in range(RECORDS):
        objectiveid = i + 1
        parentobjective = randint(0, i)
        if parentobjective == 0:
            parentobjective = 'null'

        department = randint(0, RECORDS)
        company = 1
        if (department == 0):
            department = "null"
            company = randint(1, RECORDS)
            company_obj[company].append(objectiveid)
        else:
            company = departments_company[department - 1]
            department_obj[department].append(objectiveid)
            
        title = fake.sentence(nb_words=3)
        
        termbegin = fake.date()
        termend = fake.date()
        while termbegin > termend:
            termbegin = fake.date()
            termend = fake.date()
            
        estimatedtime = randominterval()
        sql_insert = f"""INSERT INTO objective (objectiveid, parentobjective, company, department, title, termbegin, termend, estimatedtime) 
                         VALUES ({objectiveid}, {parentobjective}, {company}, {department}, \'{title}\', \'{termbegin}\', \'{termend}\', \'{estimatedtime}\');"""
        cursor.execute(sql_insert)
        connection.commit()

def generate_responsibilities(connection):
    fake = Faker()

    for i in range(RECORDS):
        responsibilityid = i + 1
        
        flag = 1
        while flag:
            employee = randint(1, RECORDS)
            objective = 1
            if workplace_emp[employee] > 1000:
                if len(department_obj[workplace_emp[employee] - 1000]) > 0:
                    objective = choice(department_obj[workplace_emp[employee] - 1000])
                    flag = 0
            else:
                if len(department_obj[workplace_emp[employee] - 1000]) > 0:
                    objective = choice(company_obj[workplace_emp[employee]])
                    flag = 0
                    
        timespent = randominterval()
        sql_insert = f"""INSERT INTO responsibility (responsibilityid, employee, objective, timespent) 
                         VALUES ({responsibilityid}, {employee}, {objective}, \'{timespent}\');"""
        cursor.execute(sql_insert)
        connection.commit()

try:
    # Подключение к существующей базе данных
    connection = psycopg2.connect(database="postgres",
                                  user="postgres",
                                  # пароль, который указали при установке PostgreSQL
                                  password="password",
                                  host="127.0.0.1",
                                  port="5432")
    connection.set_isolation_level(ISOLATION_LEVEL_AUTOCOMMIT)
    # Курсор для выполнения операций с базой данных
    cursor = connection.cursor()

    generate_users(connection)
    generate_companies(connection)
    generate_departments(connection)
    generate_employees(connection)
    generate_objectives(connection)
    generate_responsibilities(connection)

except (Exception, Error) as error:
    print("Ошибка при работе с PostgreSQL", error)
