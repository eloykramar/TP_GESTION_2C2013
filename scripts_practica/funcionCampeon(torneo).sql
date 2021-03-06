/****** Script para el comando SelectTopNRows de SSMS  ******/
--"select torneo, equipo, sum(puntos)"
--group by torneo, equipo
--having puntos=max(puntos)
--USE modelo_practica
CREATE FUNCTION CAMPEON(@TORNEO INT) RETURNS INT AS
BEGIN
DECLARE @EQ INT

SELECT TOP 1 @EQ=EQUIPO--, SUM(PUNTOS) AS PFINAL
FROM MODELO_PRACTICA.DBO.PUNTOS_PARTIDO
WHERE PART_TORNEO=@TORNEO
GROUP BY EQUIPO
ORDER BY SUM(PUNTOS) DESC

RETURN @EQ

END