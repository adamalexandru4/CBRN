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
if object_id('VX_Inhaled_IP', 'U') is not null drop table VX_Inhaled_IP
create table VX_Inhaled_IP
(
	TimePoint	int primary key not null,
	Mild		int not null,
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
insert into VX_Inhaled_IP
values
(1,		0,	2,	3,	4),
(3,		1,	2,	3,	4),
(15,	1,	2,	3,	4),
(150,	0,	2,	3,	4),
(1000,	0,	2,	2,	4),
(1940,	0,	1,	2,	4),
(8640,	0,	1,	1,	4)

if object_id('VX_PercLiq_TPS', 'U') is not null drop table VX_PercLiq_TPS
create table VX_PercLiq_TPS
(
	InjuryProfileLabel	nvarchar(64) primary key not null,
	ECt50				float not null,
	ProbitSlope			float not null,
	TLE					float not null
)
if object_id('VX_PercLiq_IP', 'U') is not null drop table VX_PercLiq_IP
create table VX_PercLiq_IP
(
	TimePoint	int primary key not null,
	Moderate	int not null,
	Severe		int not null,
	VerySevere	int not null
)
insert into VX_PercLiq_TPS
values
('Moderate',	1.2,	6,		1	),
('Severe',		2,		6,		1	),
('VerySevere',	3,		5.5,	1	)
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


if object_id('GA_Inhaled_TPS', 'U') is not null drop table GA_Inhaled_TPS
create table GA_Inhaled_TPS
(
	InjuryProfileLabel	nvarchar(64) primary key not null,
	ECt50				float not null,
	ProbitSlope			float not null,
	TLE					float not null
)
if object_id('GA_Inhaled_IP', 'U') is not null drop table GA_Inhaled_IP
create table GA_Inhaled_IP
(
	TimePoint	int primary key not null,
	Mild		int not null,
	Moderate	int not null,
	Severe		int not null,
	VerySevere	int not null
)
insert into GA_Inhaled_TPS
values
('Mild',		0.4,	4.5,	1.5	),
('Moderate',	1.2,	12,		1.5	),
('Severe',		50,		12,		1.5	),
('VerySevere',	70,		12,		1.5	)
insert into GA_Inhaled_IP
values
(1,	0,	2,	3,	4),
(3,	1,	2,	3,	4),
(15,	1,	2,	3,	4),
(150,	0,	2,	3,	 4),
(1000,	0,	2,	2,	 4),
(1940,	0,	1,	2,	 4),
(8640,	0,	1,	1,	 4)

if object_id('GB_Inhaled_TPS', 'U') is not null drop table GB_Inhaled_TPS
create table GB_Inhaled_TPS
(
	InjuryProfileLabel	nvarchar(64) primary key not null,
	ECt50				float not null,
	ProbitSlope			float not null,
	TLE					float not null
)
if object_id('GB_Inhaled_IP', 'U') is not null drop table GB_Inhaled_IP
create table GB_Inhaled_IP
(
	TimePoint	int primary key not null,
	Mild		int not null,
	Moderate	int not null,
	Severe		int not null,
	VerySevere	int not null
)
insert into GB_Inhaled_TPS
values
('Mild',		0.4,	4.5,	1.4	),
('Moderate',	1.2,	12,		1.5	),
('Severe',		25,		12,		1.5	),
('VerySevere',	33,		12,		1.5	)
insert into GB_Inhaled_IP
values
(1,	0,	2,	3,	4),
(3,	1,	2,	3,	4),
(15,	1,	2,	3,	4),
(150,	0,	2,	3,	 4),
(1000,	0,	2,	2,	 4),
(1940,	0,	1,	2,	 4),
(8640,	0,	1,	1,	 4)

if object_id('GD_Inhaled_TPS', 'U') is not null drop table GD_Inhaled_TPS
create table GD_Inhaled_TPS
(
	InjuryProfileLabel	nvarchar(64) primary key not null,
	ECt50				float not null,
	ProbitSlope			float not null,
	TLE					float not null
)
if object_id('GD_Inhaled_IP', 'U') is not null drop table GD_Inhaled_IP
create table GD_Inhaled_IP
(
	TimePoint	int primary key not null,
	Mild		int not null,
	Moderate	int not null,
	Severe		int not null,
	VerySevere	int not null
)
insert into GD_Inhaled_TPS
values
('Mild',		0.2,	4.5,	1.4	),
('Moderate',	0.6,	12,		1.5	),
('Severe',		25,		12,		1.5	),
('VerySevere',	33,		12,		1.5	)
insert into GD_Inhaled_IP
values
(1,	0,	2,	3,	4),
(3,	1,	2,	3,	4),
(15,	1,	2,	3,	4),
(150,	0,	2,	3,	 4),
(1000,	0,	2,	2,	 4),
(1940,	0,	1,	2,	 4),
(8640,	0,	1,	1,	 4)

if object_id('GF_Inhaled_TPS', 'U') is not null drop table GF_Inhaled_TPS
create table GF_Inhaled_TPS
(
	InjuryProfileLabel	nvarchar(64) primary key not null,
	ECt50				float not null,
	ProbitSlope			float not null,
	TLE					float not null
)
if object_id('GF_Inhaled_IP', 'U') is not null drop table GF_Inhaled_IP
create table GF_Inhaled_IP
(
	TimePoint	int primary key not null,
	Mild		int not null,
	Moderate	int not null,
	Severe		int not null,
	VerySevere	int not null
)
insert into GF_Inhaled_TPS
values
('Mild',		0.4,	4.5,	1.4	),
('Moderate',	1.2,	12,		1.25	),
('Severe',		31,		12,		1.25	),
('VerySevere',	41,		12,		1.25	)
insert into GF_Inhaled_IP
values
(1,	0,	2,	3,	4),
(3,	1,	2,	3,	4),
(15,	1,	2,	3,	4),
(150,	0,	2,	3,	 4),
(1000,	0,	2,	2,	 4),
(1940,	0,	1,	2,	 4),
(8640,	0,	1,	1,	 4)

if object_id('CG_Inhaled_TPS', 'U') is not null drop table CG_Inhaled_TPS
create table CG_Inhaled_TPS
(
	InjuryProfileLabel	nvarchar(64) primary key not null,
	ECt50				float not null,
	ProbitSlope			float not null,
	TLE					float not null
)
if object_id('CG_Inhaled_IP', 'U') is not null drop table CG_Inhaled_IP
create table CG_Inhaled_IP
(
	TimePoint	int primary key not null,
	Severe		int not null,
	VerySevere	int not null
)
insert into CG_Inhaled_TPS
values
('Severe',		250,		11,		1	),
('VerySevere',	1500,		11,		1	)
insert into CG_Inhaled_IP
values
(1,	0,	0),
(240,	0,	3),
(360,	0,	4),
(720,	3,	 4),
(870,	4,	 4)

if object_id('Cl2_Inhaled_TPS', 'U') is not null drop table Cl2_Inhaled_TPS
create table Cl2_Inhaled_TPS
(
	InjuryProfileLabel	nvarchar(64) primary key not null,
	ECt50				float not null,
	ProbitSlope			float not null,
	TLE					float not null
)
if object_id('Cl2_Inhaled_IP', 'U') is not null drop table Cl2_Inhaled_IP
create table Cl2_Inhaled_IP
(
	TimePoint	int primary key not null,
	Mild		int not null,
	Moderate	int not null,
	Severe		int not null,
	VerySevere	int not null
)
insert into Cl2_Inhaled_TPS
values
('Mild',		70,		10.5,	2.75	),
('Moderate',	325,	10.5,	2.75	),
('Severe',		1300,	10.5,	2.75	),
('VerySevere',	13500,	10.5,	2.75	)
insert into Cl2_Inhaled_IP
values
(1,	1,	2,	2,	3),
(120,	1,	2,	3,	4),
(135,	1,	2,	3,	4),
(360,	0,	2,	3,	 4),
(720,	0,	1,	3,	 4),
(1440,	0,	0,	3,	 4),
(10080,	0,	0,	0,	 4)

if object_id('NH3_Inhaled_TPS', 'U') is not null drop table NH3_Inhaled_TPS
create table NH3_Inhaled_TPS
(
	InjuryProfileLabel	nvarchar(64) primary key not null,
	ECt50				float not null,
	ProbitSlope			float not null,
	TLE					float not null
)
if object_id('NH3_Inhaled_IP', 'U') is not null drop table NH3_Inhaled_IP
create table NH3_Inhaled_IP
(
	TimePoint	int primary key not null,
	Mild		int not null,
	Moderate	int not null,
	Severe		int not null,
	VerySevere	int not null
)
insert into NH3_Inhaled_TPS
values
('Mild',		350,		16.5,	2	),
('Moderate',	1000,	16.5,	2	),
('Severe',		7800,	16.5,	2	),
('VerySevere',	67700,	16.5,	2	)
insert into NH3_Inhaled_IP
values
(1,	1,	2,	2,	4),
(15,	1,	2,	2,	4),
(360,	0,	2,	2,	 4),
(720,	0,	2,	3,	 4),
(4320,	0,	0,	3,	 4),
(43200,	0,	0,	0,	 4)

if object_id('AC_Inhaled_TPS', 'U') is not null drop table AC_Inhaled_TPS
create table AC_Inhaled_TPS
(
	InjuryProfileLabel	nvarchar(64) primary key not null,
	ECt50				float not null,
	ProbitSlope			float not null,
	TLE					float not null
)
if object_id('AC_Inhaled_IP', 'U') is not null drop table AC_Inhaled_IP
create table AC_Inhaled_IP
(
	TimePoint	int primary key not null,
	Mild		int not null,
	Moderate	int not null,
	Severe		int not null,
	VerySevere	int not null
)
insert into AC_Inhaled_TPS
values
('Mild',		700,	12,	2	),
('Moderate',	1100,	12,		2	),
('Severe',		1400,		12,		2	),
('VerySevere',	2600,		12,		2	)
insert into AC_Inhaled_IP
values
(1,	1,	2,	3,	4),
(10,	1,	1,	2,	4),
(15,	1,	1,	2,	4 ),
(120,	0,	1,	1,	 4),
(180,	0,	0,	1,	 4),
(480,	0,	0,	0,	 4)

if object_id('CK_Inhaled_TPS', 'U') is not null drop table CK_Inhaled_TPS
create table CK_Inhaled_TPS
(
	InjuryProfileLabel	nvarchar(64) primary key not null,
	ECt50				float not null,
	ProbitSlope			float not null,
	TLE					float not null
)
if object_id('CK_Inhaled_IP', 'U') is not null drop table CK_Inhaled_IP
create table CK_Inhaled_IP
(
	TimePoint	int primary key not null,
	Mild		int not null,
	Moderate	int not null,
	Severe		int not null,
	VerySevere	int not null
)
insert into CK_Inhaled_TPS
values
('Mild',		1200,	12,	1.45	),
('Moderate',	2100,	12,		1.45	),
('Severe',		2800,		12,		1.45	),
('VerySevere',	4700,		12,		1.45	)
insert into CK_Inhaled_IP
values
(1,	1,	2,	3,	4),
(10,1,	1,	2,	4),
(15,1,	1,	2,	4),
(120,	0,	1,	1,	4 ),
(180,	0,	0,	1,	 4),
(480,	0,	0,	0,	 4)

if object_id('H2S_Inhaled_TPS', 'U') is not null drop table H2S_Inhaled_TPS
create table H2S_Inhaled_TPS
(
	InjuryProfileLabel	nvarchar(64) primary key not null,
	ECt50				float not null,
	ProbitSlope			float not null,
	TLE					float not null
)
if object_id('H2S_Inhaled_IP', 'U') is not null drop table H2S_Inhaled_IP
create table H2S_Inhaled_IP
(
	TimePoint	int primary key not null,
	Mild		int not null,
	Moderate	int not null,
	Severe		int not null,
	VerySevere	int not null
)
insert into H2S_Inhaled_TPS
values
('Mild',		400,	18,	5.7	),
('Moderate',	1500,	18,		5.7	),
('Severe',		2200,		18,		5.7	),
('VerySevere',	3200,		18,		5.7	)
insert into H2S_Inhaled_IP
values
(1 ,	1,	2,	3,	4),
(10,	1,	1,	2,	4),
(15,	1,	1,	2,	4 ),
(60,	0,	1,	2,	 4),
(120,	0,	0,	2,	 4),
(300,	0,	0,	1,	 4),
(2880,	0,	0,	0,	4 )

if object_id('HD_Inhaled_TPS', 'U') is not null drop table HD_Inhaled_TPS
create table HD_Inhaled_TPS
(
	InjuryProfileLabel	nvarchar(64) primary key not null,
	ECt50				float not null,
	ProbitSlope			float not null,
	TLE					float not null
)
if object_id('HD_Inhaled_IP', 'U') is not null drop table HD_Inhaled_IP
create table HD_Inhaled_IP
(
	TimePoint	int primary key not null,
	Mild		int not null,
	Moderate	int not null,
	Severe		int not null,
	VerySevere	int not null
)

if object_id('HD_OcularVap_TPS', 'U') is not null drop table HD_OcularVap_TPS
create table HD_OcularVap_TPS
(
	InjuryProfileLabel	nvarchar(64) primary key not null,
	ECt50				float not null,
	ProbitSlope			float not null,
	TLE					float not null
)
if object_id('HD_OcularVap_IP', 'U') is not null drop table HD_OcularVap_IP
create table HD_OcularVap_IP
(
	TimePoint	int primary key not null,
	Mild		int not null,
	Moderate	int not null,
	Severe		int not null,
	VerySevere	int not null
)

if object_id('HD_PercVap_TPS', 'U') is not null drop table HD_PercVap_TPS
create table HD_PercVap_TPS
(
	InjuryProfileLabel	nvarchar(64) primary key not null,
	ECt50				float not null,
	ProbitSlope			float not null,
	TLE					float not null
)
if object_id('HD_PercVap_IP', 'U') is not null drop table HD_PercVap_IP
create table HD_PercVap_IP
(
	TimePoint	int primary key not null,
	Mild		int not null,
	Moderate	int not null,
	Severe		int not null,
	VerySevere	int not null
)
