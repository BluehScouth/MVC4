﻿CREATE PROCEDURE [dbo].[Usp_select_ParametroValor]
    @CodigoParametro INT ,
    @EstadoRegistro CHAR(1)
AS
Begin
    DECLARE @COLUMNAS NVARCHAR(MAX) = ''
    DECLARE @CONSULTA NVARCHAR(MAX) = ''
    DECLARE @CONDICION_ESTADO_REGISTRO NVARCHAR(100) = ''

    SELECT  @COLUMNAS = @COLUMNAS + ', ' + NOMBRE + ''
    FROM    PARAMETROSECCION
    WHERE   CODIGOPARAMETRO = @CodigoParametro
            AND ESTADOREGISTRO = 'A'

    SET @COLUMNAS = RIGHT(@COLUMNAS, LEN(@COLUMNAS) - 2)

    SET @ESTADOREGISTRO = ISNULL(@ESTADOREGISTRO, '')
    SET @CONDICION_ESTADO_REGISTRO = CASE WHEN @ESTADOREGISTRO = '' THEN ''
                                          ELSE 'AND PV.ESTADOREGISTRO = '
                                               + CHAR(39) + @ESTADOREGISTRO
                                               + CHAR(39)
                                     END

    SET @CONSULTA = N'SELECT ' + @COLUMNAS + ', EstadoRegistro
					FROM (SELECT PV.CODIGOVALOR, PS.NOMBRE, PV.VALOR, PV.ESTADOREGISTRO
					FROM PARAMETROVALOR PV, PARAMETROSECCION PS
					WHERE  ' 
							+ '
					 PV.CODIGOPARAMETRO = ' + CAST(@CODIGOPARAMETRO AS NVARCHAR) + ' '
							+ @CONDICION_ESTADO_REGISTRO + '
					AND PS.CODIGOPARAMETRO = ' + CAST(@CODIGOPARAMETRO AS NVARCHAR) + '
					AND PS.CODIGOSECCION = PV.CODIGOSECCION
					AND PV.ESTADOREGISTRO = ' + CHAR(39) + 'A' + CHAR(39) + ') AS ORIGEN
					PIVOT (MAX(VALOR) FOR NOMBRE IN (' + @COLUMNAS + ')) AS TABLA_PIVOTE
					ORDER BY ' + @COLUMNAS + ';'



    EXEC sp_executesql @CONSULTA
End
