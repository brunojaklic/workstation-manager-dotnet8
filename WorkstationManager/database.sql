DROP DATABASE IF EXISTS workstation_db;
CREATE DATABASE workstation_db;
USE workstation_db;

CREATE TABLE roles(
	id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
	role_name VARCHAR(50) NOT NULL,
    role_description VARCHAR(50)
);

CREATE TABLE users(
	id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
	first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
	username VARCHAR(50) NOT NULL UNIQUE,
	password_hash VARCHAR(100) NOT NULL,
    role_id INT NOT NULL,
    FOREIGN KEY (role_id) REFERENCES roles(id)
);

CREATE TABLE work_positions(
	id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    work_position_name VARCHAR(50) NOT NULL,
    work_position_description VARCHAR(50)
);

CREATE TABLE user_work_positions (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    user_id INT NOT NULL,
    work_position_id INT NOT NULL,
    product_name VARCHAR(100) NOT NULL,
    work_date DATE NOT NULL,
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (work_position_id) REFERENCES work_positions(id)
);