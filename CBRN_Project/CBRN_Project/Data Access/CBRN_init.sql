use master
if db_id('CBRN') is null create database CBRN

use CBRN
if object_id('VX_Inhaled_TPS', 'U') is not null drop table VX_Inhaled_TPS
create table VX_Inhaled_TPS
(
	InjuryProfileLabel	nvarchar(64) primary key not null,
	ECt50				float not null,
	ProbitSlope			float not null,
	TLE					float not null
)

if object_id('VX_PercLiq_TPS', 'U') is not null drop table VX_PercLiq_TPS
create table VX_PercLiq_TPS
(
	InjuryProfileLabel	nvarchar(64) primary key not null,
	ECt50				float not null,
	ProbitSlope			float not null,
	TLE					float not null
)

if object_id('VX_Inhaled_IP', 'U') is not null drop table VX_Inhaled_IP
create table VX_Inhaled_IP
(
	TimePoint	int primary key not null,
	Mild		int not null,
	Moderate	int not null,
	Severe		int not null,
	VerySevere	int not null
)

if object_id('VX_PercLiq_IP', 'U') is not null drop table VX_PercLiq_IP
create table VX_PercLiq_IP
(
	TimePoint	int primary key not null,
	Moderate	int not null,
	Severe		int not null,
	VerySevere	int not null
)

insert into VX_Inhaled_TPS
values
('Mild',		0.04,	4.5,	1.4	),
('Moderate',	0.12,	12,		1	),
('Severe',		9,		12,		1	),
('VerySevere',	12,		12,		1	)

insert into VX_PercLiq_TPS
values
('Moderate',	1.2,	6,		1	),
('Severe',		2,		6,		1	),
('VerySevere',	3,		5.5,	1	)

insert into VX_Inhaled_IP
values
(1,		0,	2,	3,	4),
(3,		1,	2,	3,	4),
(15,	1,	2,	3,	4),
(150,	0,	2,	3,	4),
(1000,	0,	2,	2,	4),
(1940,	0,	1,	2,	4),
(8640,	0,	1,	1,	4)

insert into VX_PercLiq_IP
values
(1,		0,	0,	0),
(8,		0,	1,	1),
(10,	1,	1,	2),
(30,	1,	1,	2),
(36,	1,	1,	4),
(51,	1,	1,	4),
(100,	1,	2,	4),
(150,	1,	3,	4),
(360,	2,	3,	4),
(1000,	1,	3,	4),
(1440,	0,	3,	4),
(2400,	0,	2,	4)