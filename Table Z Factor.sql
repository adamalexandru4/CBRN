use master
if db_id('CBRN') is null create database CBRN

use CBRN
if object_id('RDD_ISOTOPES_Z_FACTOR', 'U') is not null drop table RDD_ISOTOPES_Z_FACTOR
create table RDD_ISOTOPES_Z_FACTOR
(
	Isotope					nvarchar(64)	primary key not null,
	CloudshineWhole_Body	float not null,
	CloudshineCutaneous		float not null,

	GroundShineWhole_Body	float not null,
	GroundShineCutaneous	float not null,

	SkinCutaneous			float not null,
	InhalationWhole_Body	float not null
)

if object_id('Fallout_Z_Factors', 'U') is not null drop table Fallout_Z_Factors
create table Fallout_Z_Factors
(
	TimeAfterDetonation		float primary key not null,
	GroundShine				float not null,
	SkinContamination		float not null

)
-------INSERT--------------------------------------------------------
insert into RDD_ISOTOPES_Z_FACTOR
values																		   
('60Co ' ,  6.4 * POWER(10,2 ) , 7.3 * POWER(10,2 ) , 8.5 * POWER(10,0 ) , 9.9 * POWER(10,0 ) ,  7.8 * POWER(10,1 ) , 7.2 * POWER(10,2) ),
('90Sr ' ,  3.8 * POWER(10,-2) , 4.6 * POWER(10,1 ) , 1.0 * POWER(10,-3) , 5.0 * POWER(10,-1) ,  3.5 * POWER(10,2 ) , 3.7 * POWER(10,3) ),
('99Mo ' ,  3.7 * POWER(10,1 ) , 1.6 * POWER(10,2 ) , 5.3 * POWER(10,-1) , 1.4 * POWER(10,-1) ,  1.9 * POWER(10,2 ) , 2.0 * POWER(10,2) ),
('125I ' ,  2.6 * POWER(10,0 ) , 7.0 * POWER(10,0 ) , 1.5 * POWER(10,-1) , 4.1 * POWER(10,-1) ,  2.1 * POWER(10,0 ) , 1.5 * POWER(10,1) ),
('131I ' ,  9.2 * POWER(10,1 ) , 1.5 * POWER(10,2 ) , 1.4 * POWER(10,0 ) , 2.3 * POWER(10,0 ) ,  1.6 * POWER(10,2 ) , 6.1 * POWER(10,1) ),
('137Cs' ,  1.5 * POWER(10,2 ) , 2.3 * POWER(10,2 ) , 2.1 * POWER(10,0 ) , 6.9 * POWER(10,0 ) ,  1.6 * POWER(10,2 ) , 7.9 * POWER(10,2) ),
('192Ir' ,  2.0 * POWER(10,2 ) , 2.8 * POWER(10,2 ) , 2.9 * POWER(10,0 ) , 4.4 * POWER(10,0 ) ,  1.9 * POWER(10,2 ) , 4.3 * POWER(10,2) ),
('226Ra' ,  1.6 * POWER(10,0 ) , 2.4 * POWER(10,0 ) , 2.3 * POWER(10,-2) , 2.9 * POWER(10,-2) ,  0				    , 2.2 * POWER(10,3) ),
('238Pu' ,  2.5 * POWER(10,-2) , 2.1 * POWER(10,-1) , 3.0 * POWER(10,-3) , 3.5 * POWER(10,-2) ,  3.7 * POWER(10,-1) , 1.4 * POWER(10,4) ),
('241Am' ,  4.1 * POWER(10,0 ) , 6.5 * POWER(10,0 ) , 9.9 * POWER(10,-2) , 3.0 * POWER(10,-1) ,  1.9 * POWER(10,0 ) , 7.4 * POWER(10,3) ),
('252Cf' ,  2.5 * POWER(10,-2) , 1.6 * POWER(10,-1) , 2.6 * POWER(10,-3) , 2.1 * POWER(10,-2) ,  3.2 * POWER(10,-1) , 3.3 * POWER(10,4) )


insert into Fallout_Z_Factors
values
(0.5 ,  9.6	,	1		  ),
(1   ,  8.2	,	2.62*10-7 ),
(2   ,  7.8	,	2.59*10-7 ),
(4   ,  9.5	,	2.59*10-7 ),
(6   ,  11.7,	2.59*10-7 ),
(12  ,  13.7,	2.57*10-7 ),
(24  ,  10.9,	2.54*10-7 ),
(48  ,  8.2	,	2.49*10-7 ),
(72  ,  6.7	,	2.46*10-7 ),
(168 ,  5.0	,	2.41*10-7 ),
(336 ,  5.3	,	2.41*10-7 ),
(720 ,  6.7	,	2.43*10-7 ),
(1440,  8.5	,	2.41*10-7 ),
(2880,  9.6	,	2.38*10-7 ),
(4320,  11.0,	2.38*10-7 ),
(6480,  16.0,	2.41*10-7 ),
(8760,  26.5,	2.41*10-7 ),
(17520, 88.1,	2.43*10-7)
