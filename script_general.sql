USE GD2C2013
GO

CREATE SCHEMA YOU_SHALL_NOT_CRASH 
GO


-- creo funcion util!
CREATE FUNCTION YOU_SHALL_NOT_CRASH.Split(@String varchar(150), @Delimiter char(1))
RETURNS @Results table (word varchar(50))
AS
BEGIN
DECLARE @INDEX INT

DECLARE @SLICE varchar(200)
-- Asignar 1 a la variable que utilizaremos en el loop para no iniciar en 0. 
SELECT @INDEX = 1

WHILE @INDEX !=0
BEGIN
-- Obtenemos el índice de la primera ocurrencia del split de caracteres. 
SELECT @INDEX = CHARINDEX(@Delimiter,@STRING)
-- Ahora ponemos todo a la izquierda de el slice de la variable. 
IF @INDEX != 0
SELECT @SLICE = LEFT(@STRING,@INDEX - 1)
ELSE
SELECT @SLICE = @STRING

insert into @Results(word) values(@SLICE)

SELECT @STRING = RIGHT(@STRING,LEN(@STRING) - @INDEX)
-- Salimos del loop si terminamos la búsqueda 
IF LEN(@STRING) = 0 BREAK
END

RETURN
END
---------------------------------------------------------------------------------
GO

--CREACION TABLAS VACIAS

CREATE TABLE YOU_SHALL_NOT_CRASH.FUNCIONALIDAD (
ID_Funcionalidad int IDENTITY(1,1),
Descripcion varchar(255), 
PRIMARY KEY(ID_Funcionalidad) );

CREATE TABLE YOU_SHALL_NOT_CRASH.ROL (
ID_Rol int IDENTITY(1,1),
Descripcion varchar(255),
Activo bit DEFAULT (1),
PRIMARY KEY(ID_Rol) );

CREATE TABLE YOU_SHALL_NOT_CRASH.ROL_FUNCIONALIDAD (
ID_Rol int FOREIGN KEY REFERENCES YOU_SHALL_NOT_CRASH.ROL(ID_Rol), 
ID_Funcionalidad int FOREIGN KEY REFERENCES YOU_SHALL_NOT_CRASH.FUNCIONALIDAD(ID_Funcionalidad), 
PRIMARY KEY (ID_Rol, ID_Funcionalidad) );

CREATE TABLE YOU_SHALL_NOT_CRASH.USUARIO (
Username varchar(255),
Pass varchar(255),
DNI_Usuario numeric(18,0) UNIQUE, -- antes aca estaba ID_USUARIO, pero creo que nos conviene usar el dni, con el ID no se como hacerlo.
Intentos_Fallidos int, 
PRIMARY KEY (Username) );

CREATE TABLE YOU_SHALL_NOT_CRASH.ROL_USUARIO (
ID_Rol int FOREIGN KEY REFERENCES YOU_SHALL_NOT_CRASH.ROL(ID_Rol), 
DNI_Usuario numeric(18,0) FOREIGN KEY REFERENCES YOU_SHALL_NOT_CRASH.USUARIO(DNI_Usuario), 
PRIMARY KEY (ID_Rol, DNI_Usuario) );

CREATE TABLE YOU_SHALL_NOT_CRASH.AFILIADO (
ID_Afiliado int IDENTITY(1,1),
Nombre varchar(255),
Apellido varchar(255),
Nro_Afiliado int,
Digito_Familiar char(2),
Direccion varchar(255),
Telefono numeric(18,0),
Mail varchar(255),
Fecha_Nac DateTime,
DNI numeric(18,0)FOREIGN KEY REFERENCES YOU_SHALL_NOT_CRASH.USUARIO(DNI_Usuario),
Sexo char(1),
ID_Estado_Civil int,
Familiares_A_Cargo int,
ID_Plan NUMERIC, 
Fecha_Baja DateTime,
PRIMARY KEY (ID_Afiliado) );





---------------------------------------------------------------------------------------------------------------

--COMPLETADO TABLAS

--Creo roles que se pide por enunciado
INSERT INTO YOU_SHALL_NOT_CRASH.ROL values('Administrativo', 1)
;
INSERT INTO YOU_SHALL_NOT_CRASH.ROL values('Profesional', 1)
;
INSERT INTO YOU_SHALL_NOT_CRASH.ROL values('Afiliado', 1)
;

--Funcionalidades
INSERT INTO YOU_SHALL_NOT_CRASH.FUNCIONALIDAD values ('ABM Rol');
INSERT INTO YOU_SHALL_NOT_CRASH.FUNCIONALIDAD values ('ABM Afiliado');
INSERT INTO YOU_SHALL_NOT_CRASH.FUNCIONALIDAD values ('ABM Profesional');
INSERT INTO YOU_SHALL_NOT_CRASH.FUNCIONALIDAD values ('Registrar Agenda del medico');
INSERT INTO YOU_SHALL_NOT_CRASH.FUNCIONALIDAD values ('Compra de bonos');
INSERT INTO YOU_SHALL_NOT_CRASH.FUNCIONALIDAD values ('Pedir turno');
INSERT INTO YOU_SHALL_NOT_CRASH.FUNCIONALIDAD values ('Cancelacion de turno');
INSERT INTO YOU_SHALL_NOT_CRASH.FUNCIONALIDAD values ('Registro de llegada para atencion medica');
INSERT INTO YOU_SHALL_NOT_CRASH.FUNCIONALIDAD values ('Registro de resultado de atencion medica');
INSERT INTO YOU_SHALL_NOT_CRASH.FUNCIONALIDAD values ('Generar receta');
INSERT INTO YOU_SHALL_NOT_CRASH.FUNCIONALIDAD values ('Listado estadistico');

--Declaramos las variables con los codigos de los roles, que luego seran utilizadas
declare @cod_afiliado int
set @cod_afiliado=(SELECT ID_Rol FROM YOU_SHALL_NOT_CRASH.ROL WHERE Descripcion='Afiliado')

declare @cod_profesional int
set @cod_profesional=(SELECT ID_Rol FROM YOU_SHALL_NOT_CRASH.ROL WHERE Descripcion='Profesional')

declare @cod_administrativo int
set @cod_administrativo=(SELECT ID_Rol FROM YOU_SHALL_NOT_CRASH.ROL WHERE Descripcion='Administrativo')

--Asignar funcionalidades al Afiliado
INSERT INTO YOU_SHALL_NOT_CRASH.ROL_FUNCIONALIDAD
SELECT @cod_afiliado, ID_Funcionalidad FROM YOU_SHALL_NOT_CRASH.FUNCIONALIDAD WHERE
Descripcion='Compra de bonos' or
Descripcion='Pedir turno' or
Descripcion='Cancelacion de turno'
;

--Asignar funcionalidades al Profesional
INSERT INTO YOU_SHALL_NOT_CRASH.ROL_FUNCIONALIDAD
SELECT @cod_profesional, ID_Funcionalidad FROM YOU_SHALL_NOT_CRASH.FUNCIONALIDAD WHERE
Descripcion='Cancelacion de turno' or
Descripcion='Registro de resultado de atencion medica' or
Descripcion='Generar receta'
;

--Asignar funcionalidades al Administrador
INSERT INTO YOU_SHALL_NOT_CRASH.ROL_FUNCIONALIDAD
SELECT @cod_administrativo, ID_Funcionalidad FROM YOU_SHALL_NOT_CRASH.FUNCIONALIDAD
;

--Por decision del grupo los username seran nombre + apellido + fecha de nacimiento y el pass sera w23e encriptado con SHA-256 para todos
INSERT INTO YOU_SHALL_NOT_CRASH.USUARIO 
SELECT DISTINCT (Paciente_Nombre+Paciente_Apellido+CONVERT(VARCHAR,YEAR(Paciente_Fecha_Nac))), 'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7', Paciente_Dni, 0
FROM gd_esquema.Maestra
where Paciente_Dni is NOT NULL
union
SELECT DISTINCT (Medico_Nombre+Medico_Apellido+CONVERT(VARCHAR,YEAR(Medico_Fecha_Nac))), 'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7', Medico_Dni, 0
FROM gd_esquema.Maestra
where Medico_Dni is NOT NULL
;

--Asignamos los roles a los usuarios creados
INSERT INTO YOU_SHALL_NOT_CRASH.ROL_USUARIO 
SELECT DISTINCT @cod_afiliado, Paciente_Dni
FROM gd_esquema.Maestra
where Paciente_Dni is NOT NULL
union
SELECT DISTINCT @cod_profesional, Medico_Dni
FROM gd_esquema.Maestra
where Medico_Dni is NOT NULL
;

--Creamos un usuario administrador y le asignamos todos los roles
INSERT INTO YOU_SHALL_NOT_CRASH.USUARIO 
values ('admin', 'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7', 0, 0)
;

INSERT INTO YOU_SHALL_NOT_CRASH.ROL_USUARIO
values (@cod_administrativo, 0)
;
INSERT INTO YOU_SHALL_NOT_CRASH.ROL_USUARIO
values (@cod_afiliado, 0)
;
INSERT INTO YOU_SHALL_NOT_CRASH.ROL_USUARIO
values (@cod_profesional, 0)
;

INSERT INTO YOU_SHALL_NOT_CRASH.AFILIADO (Nombre, Apellido, Direccion, Telefono, Mail, Fecha_Nac, DNI, ID_Plan, Digito_Familiar)
SELECT DISTINCT Paciente_Nombre, Paciente_Apellido, Paciente_Direccion, Paciente_Telefono, Paciente_Mail, Paciente_Fecha_Nac, Paciente_Dni, Plan_Med_Codigo, 01
FROM gd_esquema.Maestra
;






----------------------------------------------------------------------------------------------------------------------





create table you_shall_not_crash.DIAGNOSTICO( 
ID_DIAGNOSTICO NUMERIC IDENTITY,
ID_TURNO NUMERIC,
ID_PROFESIONAL NUMERIC,
DESCRIPCION nvarchar(255)

PRIMARY KEY (ID_DIAGNOSTICO))

GO 


create table you_shall_not_crash.ITEM_DIAGNOSTICO(
ID_ITEM NUMERIC IDENTITY,
ID_DIAGNOSTICO NUMERIC,
ID_SINTOMA NUMERIC,

PRIMARY KEY (ID_ITEM),
FOREIGN KEY (ID_DIAGNOSTICO) REFERENCES  you_shall_not_crash.DIAGNOSTICO(ID_DIAGNOSTICO),
)



create table you_shall_not_crash.TIPO_ESPECIALIDAD(
CODIGO_TIPO_ESPECIALIDAD NUMERIC(18,0),
DESCRIPCION VARCHAR(255),


PRIMARY KEY (CODIGO_TIPO_ESPECIALIDAD)
)



create table you_shall_not_crash.ESPECIALIDAD(
CODIGO_ESPECIALIDAD NUMERIC(18,0),
DESCRIPCION VARCHAR(255),
CODIGO_TIPO_ESPECIALIDAD int,


PRIMARY KEY (CODIGO_ESPECIALIDAD),
)

create table you_shall_not_crash.PROFESIONAL(
ID_PROFESIONAL NUMERIC IDENTITY,
--ID_USUARIO NUMERIC,
NOMBRE VARCHAR(255),
APELLIDO VARCHAR(255),
DNI NUMERIC(18,0),
DIRECCION VARCHAR(255),
TELEFONO NUMERIC(18,0),
MAIL VARCHAR(255),
FECHA_NAC DATETIME,
SEXO VARCHAR(9),
MATRICULA INT, --VER COMO VAMOS A CREAR LA MATRICULA, POR AHORA NULL
ACTIVO bit, --SI/NO

PRIMARY KEY (ID_PROFESIONAL),
FOREIGN KEY (DNI) REFERENCES you_shall_not_crash.USUARIO(DNI_USUARIO)
)


create table you_shall_not_crash.ESPECIALIDAD_PROFESIONAL(
CODIGO_ESPECIALIDAD NUMERIC,
ID_PROFESIONAL NUMERIC,

FOREIGN KEY (CODIGO_ESPECIALIDAD) REFERENCES  you_shall_not_crash.ESPECIALIDAD(CODIGO_ESPECIALIDAD),
FOREIGN KEY (ID_PROFESIONAL) REFERENCES  you_shall_not_crash.PROFESIONAL(ID_PROFESIONAL)
)


create table you_shall_not_crash.RECETA(
ID_RECETA NUMERIC IDENTITY,
ID_DIAGNOSTICO NUMERIC,

PRIMARY KEY (ID_RECETA),
FOREIGN KEY (ID_DIAGNOSTICO) REFERENCES you_shall_not_crash.DIAGNOSTICO(ID_DIAGNOSTICO)
)


create table you_shall_not_crash.SINTOMA(
ID_SINTOMA NUMERIC IDENTITY(1,1),
DESCRIPCION VARCHAR (255),

PRIMARY KEY (ID_SINTOMA)
)


create table YOU_SHALL_NOT_CRASH.TURNO(
ID_TURNO NUMERIC IDENTITY,
NUMERO NUMERIC(18,0),
ID_PROFESIONAL NUMERIC,
ID_AFILIADO INT,
FECHA DATETIME,
FECHA_LLEGADA DATETIME,
Cancelado bit DEFAULT (0), 

PRIMARY KEY (ID_TURNO),
FOREIGN KEY (ID_PROFESIONAL) REFERENCES YOU_SHALL_NOT_CRASH.PROFESIONAL(ID_PROFESIONAL),
FOREIGN KEY (ID_AFILIADO) REFERENCES YOU_SHALL_NOT_CRASH.AFILIADO(ID_AFILIADO)
)

--PROFESIONAL--------------------
insert into you_shall_not_crash.PROFESIONAL(NOMBRE,APELLIDO,DNI,DIRECCION,TELEFONO,MAIL,FECHA_NAC) --activo,matricula y sexo null
select distinct Medico_Nombre,Medico_Apellido,Medico_Dni,Medico_Direccion,Medico_Telefono,Medico_Mail,Medico_Fecha_Nac
from gd_esquema.Maestra
where Medico_Dni is not null

--ESPECIALIDAD_PROFESIONAL-------------------------
insert into you_shall_not_crash.ESPECIALIDAD_PROFESIONAL
select distinct e.CODIGO_ESPECIALIDAD,p.ID_PROFESIONAL
from you_shall_not_crash.PROFESIONAL P join gd_esquema.Maestra M on p.DNI=m.Medico_Dni join you_shall_not_crash.ESPECIALIDAD E on M.Especialidad_Codigo=e.CODIGO_ESPECIALIDAD

--TIPO_ESPECIALIDAD-------------------------
insert into you_shall_not_crash.TIPO_ESPECIALIDAD
select distinct Tipo_Especialidad_Codigo,Tipo_Especialidad_Descripcion
from gd_esquema.Maestra
where Tipo_Especialidad_Codigo is not null
order by Tipo_Especialidad_Codigo

--ESPECIALIDAD-------------------------
insert into you_shall_not_crash.ESPECIALIDAD(CODIGO_ESPECIALIDAD,DESCRIPCION,CODIGO_TIPO_ESPECIALIDAD)
select distinct Especialidad_Codigo,Especialidad_Descripcion,Tipo_Especialidad_Codigo
from gd_esquema.Maestra
where Especialidad_Codigo is not null
order by Especialidad_Codigo;

--SINTOMA------------------------
insert into you_shall_not_crash.SINTOMA
select distinct Consulta_Sintomas
from gd_esquema.Maestra
where Consulta_Sintomas is not null;

--TURNO-----------------------------------------------------------
insert into you_shall_not_crash.TURNO 
SELECT Turno_Numero a, Max(p.ID_PROFESIONAL) b, Max(a.ID_Afiliado) c, Max(Turno_Fecha) d,
 CASE
    WHEN Max(Bono_Consulta_Numero) is not null
     THEN dateadd(MINUTE, -15, Max(Turno_Fecha))
     ELSE NULL
     END as llegada,
 0
--suponemos que a los que tienen bono consulta asignado fueron atendidos, por lo que llegaron 15 min antes
from gd_esquema.Maestra M join you_shall_not_crash.PROFESIONAL P on m.Medico_Dni=p.DNI join you_shall_not_crash.AFILIADO A on A.DNI=m.Paciente_Dni
where Turno_Numero is not null
group by Turno_Numero;

--DIAGNOSTICO---------------------------
insert into you_shall_not_crash.DIAGNOSTICO
select distinct t.id_turno, t.ID_PROFESIONAL,m.Consulta_Enfermedades
from gd_esquema.Maestra m join you_shall_not_crash.TURNO t on m.Turno_Numero=t.NUMERO
where Consulta_Enfermedades is not null and FECHA_LLEGADA is not null;


--ITEM_DIAGNOSTICO-----------------------------------------
insert into you_shall_not_crash.ITEM_DIAGNOSTICO
select distinct d.ID_DIAGNOSTICO,s.ID_SINTOMA
from you_shall_not_crash.TURNO t join you_shall_not_crash.DIAGNOSTICO d on t.ID_TURNO=d.ID_TURNO join gd_esquema.Maestra m on t.NUMERO=m.Turno_Numero join you_shall_not_crash.SINTOMA s on s.DESCRIPCION=m.Consulta_Sintomas
where Consulta_Enfermedades is not null and FECHA_LLEGADA is not null


--RECETA----------------------------
insert into you_shall_not_crash.RECETA
select d.ID_DIAGNOSTICO
from you_shall_not_crash.DIAGNOSTICO d





-----------------------------------------------------------------------------------------------





CREATE TABLE YOU_SHALL_NOT_CRASH.PLAN_MEDICO (
ID_Plan numeric(18,0),
Descripcion varchar(255), 
Precio_plan numeric(18,2),
Precio_bono_consulta numeric(18,2),
Precio_bono_farmacia numeric(18,2),

PRIMARY KEY(ID_Plan) );


CREATE TABLE YOU_SHALL_NOT_CRASH.MEDICAMENTO (
ID_Medicamento int identity(1,1),
Descripcion varchar(255),

PRIMARY KEY(ID_Medicamento) );


CREATE TABLE YOU_SHALL_NOT_CRASH.BONO_CONSULTA (
ID_Bono_Consulta numeric(18,0) ,
Fecha_Emision datetime,
ID_Afiliado int ,
ID_Turno NUMERIC ,

PRIMARY KEY(ID_Bono_Consulta),
FOREIGN KEY (ID_Afiliado) REFERENCES YOU_SHALL_NOT_CRASH.AFILIADO(ID_Afiliado),
FOREIGN KEY (ID_Turno) REFERENCES YOU_SHALL_NOT_CRASH.TURNO(ID_Turno) );


CREATE TABLE YOU_SHALL_NOT_CRASH.BONO_FARMACIA (
ID_Bono_Farmacia numeric(18,0) ,
Fecha_Emision datetime,
ID_Afiliado int ,
ID_Receta_Medica NUMERIC ,
Fecha_Prescripcion_Medica datetime,
--ACA NO DEBERIA IR LA FECHA DE VENCIMIENTO DEL BONO?

PRIMARY KEY(ID_Bono_Farmacia),
FOREIGN KEY (ID_Afiliado) REFERENCES YOU_SHALL_NOT_CRASH.AFILIADO(ID_Afiliado),
FOREIGN KEY (ID_Receta_Medica) REFERENCES YOU_SHALL_NOT_CRASH.RECETA(ID_Receta) );


CREATE TABLE YOU_SHALL_NOT_CRASH.ITEM_BONO_FARMACIA (
ID_Bono_Farmacia numeric(18,0),
ID_Item int ,
ID_Medicamento int ,
Cantidad int,

PRIMARY KEY(ID_Bono_Farmacia,ID_Item),
FOREIGN KEY (ID_Bono_Farmacia) REFERENCES YOU_SHALL_NOT_CRASH.BONO_FARMACIA(ID_Bono_Farmacia),
FOREIGN KEY (ID_Medicamento) REFERENCES YOU_SHALL_NOT_CRASH.MEDICAMENTO(ID_Medicamento) );








--PLAN MEDICO-----------------------------------------------------
INSERT INTO YOU_SHALL_NOT_CRASH.PLAN_MEDICO (ID_Plan,Descripcion,Precio_bono_consulta,Precio_bono_farmacia)
SELECT DISTINCT Plan_Med_Codigo, Plan_Med_Descripcion, Plan_Med_Precio_Bono_Consulta, Plan_Med_Precio_Bono_Farmacia 
FROM gd_esquema.Maestra
;


--MEDICAMENTO-----------------------------------------------------
INSERT INTO YOU_SHALL_NOT_CRASH.MEDICAMENTO (Descripcion)
SELECT DISTINCT Bono_Farmacia_Medicamento 
FROM gd_esquema.Maestra 
WHERE Bono_Farmacia_Medicamento IS NOT NULL
;

--BONO CONSULTA---------------------------------------------------
INSERT INTO YOU_SHALL_NOT_CRASH.BONO_CONSULTA (ID_Bono_Consulta, Fecha_Emision)
SELECT DISTINCT Bono_Consulta_Numero, Bono_Consulta_Fecha_Impresion
FROM gd_esquema.Maestra
WHERE Bono_Consulta_Fecha_Impresion IS NOT NULL AND 
	  Bono_Consulta_Numero IS NOT NULL
;

--BONO FARMACIA---------------------------------------------------
INSERT INTO YOU_SHALL_NOT_CRASH.BONO_FARMACIA(Fecha_Emision,ID_Bono_Farmacia)
SELECT DISTINCT Bono_Farmacia_Fecha_Impresion, Bono_Farmacia_Numero --el distinct lo agregué para q no pinche!
FROM gd_esquema.Maestra
WHERE Bono_Farmacia_Fecha_Impresion IS NOT NULL AND
	  Bono_Farmacia_Numero IS NOT NULL
;  


--ITEM BONO FARMACIA---------------------------------------------
--INSERT INTO YOU_SHALL_NOT_CRASH.ITEM_BONO_FARMACIA (ID_Bono_Farmacia)
--SELECT Bono_Farmacia_Numero FROM gd_esquema.Maestra
--WHERE Bono_Farmacia_Numero IS NOT NULL AND
--	  Bono_Farmacia_Medicamento IS NOT NULL
--;





---------------------------------------------------------------------------------------

CREATE TABLE YOU_SHALL_NOT_CRASH.ESTADO_CIVIL (
ID_Estado_Civil int identity(1,1),
Descripcion varchar(255),
PRIMARY KEY(ID_Estado_Civil) );

INSERT INTO YOU_SHALL_NOT_CRASH.ESTADO_CIVIL VALUES ('Soltero/a');
INSERT INTO YOU_SHALL_NOT_CRASH.ESTADO_CIVIL VALUES ('Casado/a');
INSERT INTO YOU_SHALL_NOT_CRASH.ESTADO_CIVIL VALUES ('Viudo/a');
INSERT INTO YOU_SHALL_NOT_CRASH.ESTADO_CIVIL VALUES ('Concubinato');
INSERT INTO YOU_SHALL_NOT_CRASH.ESTADO_CIVIL VALUES ('Divorciado/a');

--Cancelaciones: Se tomará para la migracion el día 01/01/2013 como fecha default de cancelacion.

CREATE TABLE YOU_SHALL_NOT_CRASH.CANCELACION_TURNO (
ID_Cancelacion int identity(1,1),
Tipo_Cancelacion varchar(30),
Detalle varchar(255),
Fecha datetime,
ID_Turno numeric,

PRIMARY KEY (ID_Cancelacion),
FOREIGN KEY (ID_Turno) REFERENCES YOU_SHALL_NOT_CRASH.TURNO (ID_TURNO))


-- Se consideran "cancelados" los turnos anteriores a la fecha actual a los que no se haya presentado el paciente.
-- Se considera que al no tener detallado los motivos el tipo de cancelacion se asume "por el paciente" y "sin motivo" en
-- el detalle.
-- Por nueva restriccion de dominio se cancelan todos los turnos agendados para días Domingos con tipo de cancelacion
-- "por el profesional" con motivo "cambio de agenda del profesional".
INSERT INTO YOU_SHALL_NOT_CRASH.CANCELACION_TURNO
SELECT 
   CASE
    WHEN FECHA>GETDATE()
     THEN 'CANCELA_PROFESIONAL'
     ELSE 'CANCELA_PACIENTE'
     END as Tipo_Cancelacion,
   CASE
    WHEN FECHA>GETDATE()
     THEN 'CAMBIO DE AGENDA DEL PROFESIONAL'
     ELSE 'SIN MOTIVO'
     END as Detalle,
   '01/01/2013' Fecha, ID_TURNO as IdTurno 
FROM YOU_SHALL_NOT_CRASH.TURNO
WHERE (FECHA_LLEGADA IS NULL AND FECHA<GETDATE()) OR (datepart(dw,FECHA)=7 AND FECHA>GETDATE()) ;
 
--Ahora actualizo el booleano de los turnos que fueron cancelados
UPDATE YOU_SHALL_NOT_CRASH.TURNO SET Cancelado=1 WHERE ID_TURNO IN (SELECT ID_TURNO FROM YOU_SHALL_NOT_CRASH.CANCELACION_TURNO)


--AGENDA-----

CREATE TABLE YOU_SHALL_NOT_CRASH.AGENDA (
Id_Agenda int identity(1,1),	
Id_Profesional numeric,
Dia int,
Hora_Inicio int,
Hora_Fin int,

PRIMARY KEY (Id_Agenda),
FOREIGN KEY (Id_Profesional) REFERENCES YOU_SHALL_NOT_CRASH.PROFESIONAL (ID_Profesional));


--ESTRATEGIA:
--DADO QUE EN LA TABLA MAESTRA TODOS LOS HORARIOS DE ATENTION DE TODOS LOS MEDICOS SON DE 8 A 18HS Y DE DOMINGOS A JUEVES.
--TENIENDO QUE RESTRINGIR LA ATENCION SOLO DE LUNES A SABADOS, CANCELAMOS TODOS LOS TURNOS FUTUROS DE LOS DOMINGOS.
--Y DEJAMOS COMO AGENDA, LOS HORARIOS QUE MANTENÍAN LOS PROFESIONALES HASTA EL MOMENTO EXCEPTUANDO ESTE ULTIMO DIA.
--POR ENDE, PARA TODOS LOS MEDICOS EL HORARIO DE ATENCION SERÁ DE LUNES A JUEVES DE 8 A 18HS PUDIENDO SER AJUSTADO DESDE EL ABM CORRESPONDIENTE. 

INSERT INTO YOU_SHALL_NOT_CRASH.AGENDA
SELECT p.Id_Profesional, datepart(dw,TURNO_FECHA), 1800, 800 --mAX(dateNAME(dw,TURNO_FECHA)),MAX(TURNO_FECHA), Max(CONVERT(INT,REPLACE(CONVERT(VARCHAR(5),TURNO_FECHA,108), ':', ''))), Min(CONVERT(INT,REPLACE(CONVERT(VARCHAR(5),TURNO_FECHA,108), ':', '')))
FROM  gd_esquema.Maestra join YOU_SHALL_NOT_CRASH.PROFESIONAL p on gd_esquema.Maestra.Medico_Dni=p.DNI
WHERE TURNO_FECHA IS NOT NULL AND Medico_Dni IS NOT NULL AND datepart(dw,TURNO_FECHA)!=7 
GROUP BY p.Id_Profesional, datepart(dw,TURNO_FECHA)



















 
 --AGREGAMOS LAS FOREING KEYS FALTANTES:
 
ALTER TABLE YOU_SHALL_NOT_CRASH.AFILIADO
ADD FOREIGN KEY (ID_Estado_Civil)
REFERENCES YOU_SHALL_NOT_CRASH.ESTADO_CIVIL(ID_Estado_Civil);

ALTER TABLE YOU_SHALL_NOT_CRASH.AFILIADO
ADD FOREIGN KEY (ID_Plan)
REFERENCES YOU_SHALL_NOT_CRASH.PLAN_MEDICO(ID_Plan);

ALTER TABLE YOU_SHALL_NOT_CRASH.DIAGNOSTICO
ADD FOREIGN KEY (ID_TURNO) REFERENCES you_shall_not_crash.TURNO(ID_TURNO);

ALTER TABLE YOU_SHALL_NOT_CRASH.ITEM_DIAGNOSTICO
ADD FOREIGN KEY (ID_SINTOMA) REFERENCES you_shall_not_crash.SINTOMA(ID_SINTOMA);

--SELECT * FROM YOU_SHALL_NOT_CRASH.Split('med1+med2+med3','+');


---------------------------------------------------------------------
----------------------FUNCIONES Y SPS--------------------------------
---------------------------------------------------------------------

create procedure YOU_SHALL_NOT_CRASH.login(@usuario nvarchar(255), @pass nvarchar(255), @respuesta nvarchar(255) output)
AS 
BEGIN                  
 
declare @existeUsuario INT = (SELECT COUNT(*) FROM YOU_SHALL_NOT_CRASH.USUARIO WHERE Username = @usuario);                   
                   
if (@existeUsuario = 1)
begin                                          
	declare @cantidadIntentosFallidos INT = (SELECT Intentos_Fallidos FROM YOU_SHALL_NOT_CRASH.USUARIO WHERE Username = @usuario);
	    
	if (@cantidadIntentosFallidos < 3)
	begin
		declare @existeUsuarioyContraseña INT = (SELECT COUNT(*) FROM YOU_SHALL_NOT_CRASH.USUARIO WHERE Username = @usuario and Pass = @pass);
		
		if (@existeUsuarioyContraseña = 1)
		begin
			UPDATE YOU_SHALL_NOT_CRASH.USUARIO SET Intentos_Fallidos=0 WHERE Username = @usuario;
			declare @existeRol INT = (SELECT COUNT(*) FROM YOU_SHALL_NOT_CRASH.USUARIO u join YOU_SHALL_NOT_CRASH.ROL_USUARIO ru on (u.DNI_Usuario = ru.DNI_Usuario) WHERE Username = @usuario)
			
			if (@existeRol = 0)
			begin
				set @respuesta='El usuario no tiene asignado un rol, o el rol ha sido inhabilitado'
			end
			else
			begin			
				set @respuesta='abrir sesion'
			end

		end
		else
		begin
			UPDATE YOU_SHALL_NOT_CRASH.USUARIO SET Intentos_Fallidos=(Intentos_Fallidos+1) WHERE Username = @usuario;
			set @cantidadIntentosFallidos = (@cantidadIntentosFallidos + 1);
			declare @cantidadIntentosFallidosString nvarchar(255) = @cantidadIntentosFallidos;
			
			set @respuesta = 'Contraseña incorrecta, vuelva a intentarlo;Cantidad de intentos fallidos: ' + (@cantidadIntentosFallidosString);
		end

	end
	else
	begin
		set @respuesta = 'Su usuario esta bloqueado, por sobrepasar la cantidad de logueos incorrectos';
	end  
end
else
begin 
set @respuesta = 'No existe el usuario, vuelva a intentarlo';                              
end

END