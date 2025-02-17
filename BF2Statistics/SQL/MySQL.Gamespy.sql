CREATE TABLE `accounts` (
	`id` int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
	`name` VARCHAR(20) NOT NULL UNIQUE,
	`password` VARCHAR(32) NOT NULL,
	`email` VARCHAR(50) NOT NULL,
	`country` VARCHAR(4) NOT NULL,
	`lastip` VARCHAR(20) DEFAULT NULL,
	`session` int(11) UNSIGNED DEFAULT 0
) DEFAULT CHARSET=latin1;

CREATE TABLE `_version` (
  `dbver` VARCHAR(4) NOT NULL DEFAULT 0,
  `dbdate` int(10) unsigned NOT NULL DEFAULT 0,
  PRIMARY KEY (`dbver`)
) DEFAULT CHARSET=latin1;

INSERT INTO _version VALUES("2.1", 0);