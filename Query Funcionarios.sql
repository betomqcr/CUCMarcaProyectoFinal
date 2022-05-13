declare 
	@anio int = 2022,
	@periodo int = 2;

SELECT F.Nombre, F.Apellido, F.Correo, FA.CodigoFuncionario, FA.FuncionarioID, FA.AreaID, H.HorarioId, HD.Dia, HD.HoraIngreso, HD.MinutoIngreso, HD.HoraSalida, HD.MinutoSalida
FROM Funcionario F 
INNER JOIN FuncionarioArea FA ON F.FuncionarioID = FA.FuncionarioID
INNER JOIN Horario H ON FA.CodigoFuncionario = H.CodigoFuncionario
INNER JOIN HorarioDetalle HD on HD.HorarioID = H.HorarioId
WHERE F.Estado = 1 --el funcionario debe estar activo
AND H.Estado = 1 --El horario debe estar vigente
AND H.Anio = @anio
AND H.Cuatrimestre = @periodo
AND HD.Dia = 2;

select CodigoFuncionario,  Min(FechaAsistencia) from Asistencia
where CodigoFuncionario='112780286'
group by CodigoFuncionario