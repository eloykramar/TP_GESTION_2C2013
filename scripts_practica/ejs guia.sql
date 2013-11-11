--EJercicios de la guia

--8
select JUGA_DNI, JUGA_APELLIDO, JUGA_NOMBRES, EQUI_NOMBRE, EQUI_DIVISION
from LESION_JUGADOR 
	join TIPO_LESION on (LEJU_LESION = TILE_CODIGO)
	join EQUIPO_JUGADOR on (LEJU_JUGADOR = EQJU_JUGADOR)
	join JUGADOR on (EQJU_JUGADOR = JUGA_DNI)
	join EQUIPO on (EQJU_EQUIPO = EQUI_CODIGO)
where TIPO_LESION.TILE_DIAS_RECUPERACION > 7


--11

select PART_CODIGO, local.EQUI_NOMBRE, visitante.EQUI_NOMBRE, 
	SUM(CASE WHEN (
		(PART_LOCAL = DEPA_EQUIPO and DEPA_ACCION = 1) or 
		(PART_VISITANTE = DEPA_EQUIPO and DEPA_ACCION = 10)) then 1 
		else 0 end) as goles_local,
	SUM(CASE WHEN (
		(PART_VISITANTE = DEPA_EQUIPO and DEPA_ACCION = 1) or 
		(PART_LOCAL = DEPA_EQUIPO and DEPA_ACCION = 10)) then 1 
		else 0 end) as goles_visitante

from PARTIDO join 
	DETALLE_PARTIDO on (PART_CODIGO = DEPA_PARTIDO) join
	EQUIPO as LOCAL on(LOCAL.EQUI_CODIGO = PART_LOCAL) join
	EQUIPO as VISITANTE on (VISITANTE.EQUI_CODIGO = PART_VISITANTE)
	
where PART_DURACION is not NULL
	
group by PART_CODIGO, local.EQUI_NOMBRE, visitante.EQUI_NOMBRE



--12

select PART_TORNEO, PART_CODIGO, EQUI_CODIGO, EQUI_NOMBRE, 
	case when (select COUNT(*)
				from DETALLE_PARTIDO
				where DEPA_PARTIDO = PART_CODIGO and 
					((DEPA_ACCION = 1 and
					DEPA_EQUIPO = PART_LOCAL) or 
					(DEPA_ACCION = 10 and
					DEPA_EQUIPO = PART_VISITANTE))) > (select COUNT(*)
														from DETALLE_PARTIDO
														where DEPA_PARTIDO = PART_CODIGO and 
															((DEPA_ACCION = 1 and
															DEPA_EQUIPO = PART_VISITANTE) or 
															(DEPA_ACCION = 10 and
															DEPA_EQUIPO = PART_LOCAL))) then 3
	      when (select COUNT(*)
				from DETALLE_PARTIDO
				where DEPA_PARTIDO = PART_CODIGO and 
					((DEPA_ACCION = 1 and
					DEPA_EQUIPO = PART_LOCAL) or 
					(DEPA_ACCION = 10 and
					DEPA_EQUIPO = PART_VISITANTE))) = (select COUNT(*)
														from DETALLE_PARTIDO
														where DEPA_PARTIDO = PART_CODIGO and 
															((DEPA_ACCION = 1 and
															DEPA_EQUIPO = PART_VISITANTE) or 
															(DEPA_ACCION = 10 and
															DEPA_EQUIPO = PART_LOCAL))) then 1	else 0	end												

from EQUIPO join PARTIDO on (EQUI_CODIGO = PART_LOCAL)
where PART_DURACION is not NULL

UNION

select PART_TORNEO, PART_CODIGO, EQUI_CODIGO, EQUI_NOMBRE, 
	case when (select COUNT(*)
				from DETALLE_PARTIDO
				where DEPA_PARTIDO = PART_CODIGO and 
					((DEPA_ACCION = 1 and
					DEPA_EQUIPO = PART_LOCAL) or 
					(DEPA_ACCION = 10 and
					DEPA_EQUIPO = PART_VISITANTE))) < (select COUNT(*)
														from DETALLE_PARTIDO
														where DEPA_PARTIDO = PART_CODIGO and 
															((DEPA_ACCION = 1 and
															DEPA_EQUIPO = PART_VISITANTE) or 
															(DEPA_ACCION = 10 and
															DEPA_EQUIPO = PART_LOCAL))) then 3
	      when (select COUNT(*)
				from DETALLE_PARTIDO
				where DEPA_PARTIDO = PART_CODIGO and 
					((DEPA_ACCION = 1 and
					DEPA_EQUIPO = PART_LOCAL) or 
					(DEPA_ACCION = 10 and
					DEPA_EQUIPO = PART_VISITANTE))) = (select COUNT(*)
														from DETALLE_PARTIDO
														where DEPA_PARTIDO = PART_CODIGO and 
															((DEPA_ACCION = 1 and
															DEPA_EQUIPO = PART_VISITANTE) or 
															(DEPA_ACCION = 10 and
															DEPA_EQUIPO = PART_LOCAL))) then 1	else 0	end												

from EQUIPO join PARTIDO on (EQUI_CODIGO = PART_VISITANTE)
where PART_DURACION is not NULL
order by 1,2,3


--vista puntos por partido 

go
create view PUNTOS_PARTIDO as(
select PART_TORNEO, PART_CODIGO, EQUI_CODIGO, EQUI_NOMBRE, 
	case when (select COUNT(*)
				from DETALLE_PARTIDO
				where DEPA_PARTIDO = PART_CODIGO and 
					((DEPA_ACCION = 1 and
					DEPA_EQUIPO = PART_LOCAL) or 
					(DEPA_ACCION = 10 and
					DEPA_EQUIPO = PART_VISITANTE))) > (select COUNT(*)
														from DETALLE_PARTIDO
														where DEPA_PARTIDO = PART_CODIGO and 
															((DEPA_ACCION = 1 and
															DEPA_EQUIPO = PART_VISITANTE) or 
															(DEPA_ACCION = 10 and
															DEPA_EQUIPO = PART_LOCAL))) then 3
	      when (select COUNT(*)
				from DETALLE_PARTIDO
				where DEPA_PARTIDO = PART_CODIGO and 
					((DEPA_ACCION = 1 and
					DEPA_EQUIPO = PART_LOCAL) or 
					(DEPA_ACCION = 10 and
					DEPA_EQUIPO = PART_VISITANTE))) = (select COUNT(*)
														from DETALLE_PARTIDO
														where DEPA_PARTIDO = PART_CODIGO and 
															((DEPA_ACCION = 1 and
															DEPA_EQUIPO = PART_VISITANTE) or 
															(DEPA_ACCION = 10 and
															DEPA_EQUIPO = PART_LOCAL))) then 1	else 0	end	as puntos										

from EQUIPO join PARTIDO on (EQUI_CODIGO = PART_LOCAL)
where PART_DURACION is not NULL

UNION

select PART_TORNEO, PART_CODIGO, EQUI_CODIGO, EQUI_NOMBRE, 
	case when (select COUNT(*)
				from DETALLE_PARTIDO
				where DEPA_PARTIDO = PART_CODIGO and 
					((DEPA_ACCION = 1 and
					DEPA_EQUIPO = PART_LOCAL) or 
					(DEPA_ACCION = 10 and
					DEPA_EQUIPO = PART_VISITANTE))) < (select COUNT(*)
														from DETALLE_PARTIDO
														where DEPA_PARTIDO = PART_CODIGO and 
															((DEPA_ACCION = 1 and
															DEPA_EQUIPO = PART_VISITANTE) or 
															(DEPA_ACCION = 10 and
															DEPA_EQUIPO = PART_LOCAL))) then 3
	      when (select COUNT(*)
				from DETALLE_PARTIDO
				where DEPA_PARTIDO = PART_CODIGO and 
					((DEPA_ACCION = 1 and
					DEPA_EQUIPO = PART_LOCAL) or 
					(DEPA_ACCION = 10 and
					DEPA_EQUIPO = PART_VISITANTE))) = (select COUNT(*)
														from DETALLE_PARTIDO
														where DEPA_PARTIDO = PART_CODIGO and 
															((DEPA_ACCION = 1 and
															DEPA_EQUIPO = PART_VISITANTE) or 
															(DEPA_ACCION = 10 and
															DEPA_EQUIPO = PART_LOCAL))) then 1	else 0	end	as puntos										
 
from EQUIPO join PARTIDO on (EQUI_CODIGO = PART_VISITANTE)
where PART_DURACION is not NULL
)


--tabla de posiciones de un determinado torneo

select EQUI_NOMBRE,sum(puntos) from PUNTOS_PARTIDO
where PART_TORNEO = 1
group by EQUI_NOMBRE


--13 no joineo con los arbitros, xq no devuelve nada

select juga_dni, juga_nombres, juga_apellido,
(select COUNT(*)
	from JUGADOR_PARTIDO
	where JUPA_JUGADOR = JUGA_DNI and
		JUPA_TITULAR = 'S') as veces_titular, 
(select COUNT(*)
	from JUGADOR_PARTIDO 
		join PARTIDO on (JUPA_PARTIDO = PART_CODIGO)
		join EQUIPO as local on (local.EQUI_CODIGO = PART_LOCAL)
		join EQUIPO as visitante on (visitante.EQUI_CODIGO = PART_VISITANTE)
	where JUPA_JUGADOR = local.EQUI_DT or JUPA_JUGADOR = visitante.EQUI_DT and
		JUPA_JUGADOR = JUGA_DNI) as jugador_dt,
case when (select COUNT(*)
			from JUGADOR_PARTIDO
			where JUPA_JUGADOR = JUGA_DNI and
				JUPA_TITULAR = 'S') >  
		(select COUNT(*)
			from JUGADOR_PARTIDO 
				join PARTIDO on (JUPA_PARTIDO = PART_CODIGO)
				join EQUIPO as local on (local.EQUI_CODIGO = PART_LOCAL)
				join EQUIPO as visitante on (visitante.EQUI_CODIGO = PART_VISITANTE)
			where JUPA_JUGADOR = local.EQUI_DT or JUPA_JUGADOR = visitante.EQUI_DT and
				JUPA_JUGADOR = JUGA_DNI)  then 'jugador titular' else 'tecnico jugador' end
		  
from DIRECTOR_TECNICO 
	join JUGADOR on (DITE_DNI=JUGA_DNI) 
--	join ARBITRO on (ARBI_DNI=DITE_DNI)


----------------------------------------------------------------------------------------------------------------------
--pl-sql

--5

CREATE TRIGGER JUGADOR_MISMA_DIVISION
ON EQUIPO_JUGADOR
INSTEAD OF 
INSERT AS
BEGIN
	DECLARE @A INT = 0
	IF NOT EXISTS (SELECT * FROM inserted
				WHERE (SELECT EQUI_DIVISION 
						FROM EQUIPO
						WHERE EQUI_CODIGO	= inserted.EQJU_EQUIPO)
						IN (SELECT EQUI_DIVISION FROM EQUIPO_JUGADOR E JOIN EQUIPO ON (E.EQJU_EQUIPO = EQUI_CODIGO)
						WHERE E.EQJU_JUGADOR = inserted.EQJU_JUGADOR))
	BEGIN 	
		INSERT INTO EQUIPO_JUGADOR 
		SELECT * FROM INSERTED	
	END	
	ELSE	
	BEGIN	
		PRINT 'NO SE INSERTO'	
	END
END


--11

go
create function getCampeon(@codTorneo int)
RETURNS varchar(255)
begin
return (select top 1 EQUI_NOMBRE
		from PUNTOS_PARTIDO
		where PART_TORNEO = @codTorneo
		group by EQUI_NOMBRE
		order by sum(puntos) desc)
end

declare @a varchar(255) = dbo.getCampeon(1)
print @a
