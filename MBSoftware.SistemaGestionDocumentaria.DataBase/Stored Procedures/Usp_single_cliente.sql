CREATE PROCEDURE [dbo].[Usp_single_cliente]
	  @ClienteId BIGINT
AS
		SELECT ClienteId,Nombre,Apellidos,Email,Telefono,Direccion,CodigoPais,CodigoDocumento,NumeroDocumento FROM Cliente
	where ClienteId = @ClienteId;
RETURN 0
