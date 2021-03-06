CREATE VIEW PUNTOS_PARTIDO AS
SELECT     PART_TORNEO as TORNEO, PART_CODIGO as PARTIDO, PART_LOCAL EQUIPO, CASE WHEN
                          ((SELECT     COUNT(*)
                              FROM         (PARTIDO INNER JOIN
                                                    DETALLE_PARTIDO ON
                                                    (DEPA_PARTIDO = PART_CODIGO) AND
                                                    (PART_CODIGO = p.PART_CODIGO))
                              WHERE     (DEPA_ACCION = 1 AND DEPA_EQUIPO = PART_LOCAL) OR
                                                    (DEPA_ACCION = 10 AND DEPA_EQUIPO = PART_VISITANTE)) >
                          (SELECT     COUNT(*)
                            FROM          (PARTIDO INNER JOIN
                                                   DETALLE_PARTIDO ON (DEPA_PARTIDO = PART_CODIGO) AND
                                                    (PART_CODIGO = p.PART_CODIGO))
                                                   
                            WHERE      (DEPA_ACCION = 1 AND DEPA_EQUIPO = PART_VISITANTE) OR
                                                   (DEPA_ACCION = 10 AND DEPA_EQUIPO = PART_LOCAL))) THEN 3 WHEN
                          ((SELECT     COUNT(*)
                              FROM         (PARTIDO INNER JOIN
                                                    DETALLE_PARTIDO ON (DEPA_PARTIDO = PART_CODIGO) AND
                                                    (PART_CODIGO = p.PART_CODIGO))
                                                   
                              WHERE     (DEPA_ACCION = 1 AND DEPA_EQUIPO = PART_LOCAL) OR
                                                    (DEPA_ACCION = 10 AND DEPA_EQUIPO = PART_VISITANTE)) <
                          (SELECT     COUNT(*)
                            FROM          (PARTIDO INNER JOIN
                                                   DETALLE_PARTIDO ON (DEPA_PARTIDO = PART_CODIGO) AND
                                                    (PART_CODIGO = p.PART_CODIGO))
                                                   
                            WHERE      (DEPA_ACCION = 1 AND DEPA_EQUIPO = PART_VISITANTE) OR
                                                   (DEPA_ACCION = 10 AND DEPA_EQUIPO = PART_LOCAL))) THEN 0 WHEN
                          ((SELECT     COUNT(*)
                              FROM         (PARTIDO INNER JOIN
                                                    DETALLE_PARTIDO ON (DEPA_PARTIDO = PART_CODIGO) AND
                                                    (PART_CODIGO = p.PART_CODIGO))
                                                   
                              WHERE     (DEPA_ACCION = 1 AND DEPA_EQUIPO = PART_LOCAL) OR
                                                    (DEPA_ACCION = 10 AND DEPA_EQUIPO = PART_VISITANTE)) =
                          (SELECT     COUNT(*)
                            FROM          (PARTIDO INNER JOIN
                                                   DETALLE_PARTIDO ON (DEPA_PARTIDO = PART_CODIGO) AND
                                                    (PART_CODIGO = p.PART_CODIGO))
                                                   
                            WHERE      (DEPA_ACCION = 1 AND DEPA_EQUIPO = PART_VISITANTE) OR
                                                   (DEPA_ACCION = 10 AND DEPA_EQUIPO = PART_LOCAL))) THEN 1 END AS PUNTOS
FROM         PARTIDO p
UNION
SELECT     PART_TORNEO, PART_CODIGO, PART_VISITANTE EQUIPO, CASE WHEN
                          ((SELECT     COUNT(*)
                              FROM         (PARTIDO INNER JOIN
                                                    DETALLE_PARTIDO ON (DEPA_PARTIDO = PART_CODIGO) AND
                                                    (PART_CODIGO = p.PART_CODIGO)
                                                    )
                              WHERE     (DEPA_ACCION = 1 AND DEPA_EQUIPO = PART_LOCAL) OR
                                                    (DEPA_ACCION = 10 AND DEPA_EQUIPO = PART_VISITANTE)) <
                          (SELECT     COUNT(*)
                            FROM          (PARTIDO INNER JOIN
                                                   DETALLE_PARTIDO ON (DEPA_PARTIDO = PART_CODIGO) AND
                                                    (PART_CODIGO = p.PART_CODIGO)
                                                    )
                            WHERE      (DEPA_ACCION = 1 AND DEPA_EQUIPO = PART_VISITANTE) OR
                                                   (DEPA_ACCION = 10 AND DEPA_EQUIPO = PART_LOCAL))) THEN 3 WHEN
                          ((SELECT     COUNT(*)
                              FROM         (PARTIDO INNER JOIN
                                                    DETALLE_PARTIDO ON(DEPA_PARTIDO = PART_CODIGO) AND
                                                    (PART_CODIGO = p.PART_CODIGO)
                                                    )
                              WHERE     (DEPA_ACCION = 1 AND DEPA_EQUIPO = PART_LOCAL) OR
                                                    (DEPA_ACCION = 10 AND DEPA_EQUIPO = PART_VISITANTE)) >
                          (SELECT     COUNT(*)
                            FROM          (PARTIDO INNER JOIN
                                                   DETALLE_PARTIDO ON (DEPA_PARTIDO = PART_CODIGO) AND
                                                    (PART_CODIGO = p.PART_CODIGO)
                                                    )
                            WHERE      (DEPA_ACCION = 1 AND DEPA_EQUIPO = PART_VISITANTE) OR
                                                   (DEPA_ACCION = 10 AND DEPA_EQUIPO = PART_LOCAL))) THEN 0 WHEN
                          ((SELECT     COUNT(*)
                              FROM         (PARTIDO INNER JOIN
                                                    DETALLE_PARTIDO ON (DEPA_PARTIDO = PART_CODIGO) AND
                                                    (PART_CODIGO = p.PART_CODIGO)
                                                    )
                              WHERE     (DEPA_ACCION = 1 AND DEPA_EQUIPO = PART_LOCAL) OR
                                                    (DEPA_ACCION = 10 AND DEPA_EQUIPO = PART_VISITANTE)) =
                          (SELECT     COUNT(*)
                            FROM          (PARTIDO INNER JOIN
                                                   DETALLE_PARTIDO ON (DEPA_PARTIDO = PART_CODIGO) AND
                                                    (PART_CODIGO = p.PART_CODIGO)
                                                    )
                            WHERE      (DEPA_ACCION = 1 AND DEPA_EQUIPO = PART_VISITANTE) OR
                                                   (DEPA_ACCION = 10 AND DEPA_EQUIPO = PART_LOCAL))) THEN 1 END AS PUNTOS
FROM PARTIDO P
