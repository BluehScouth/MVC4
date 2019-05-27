CREATE PROCEDURE [dbo].[Usp_update_cliente]
    @ClienteId BIGINT,
	@Nombre NVARCHAR(100),
	@Apellidos NVARCHAR(100),
	@Email NVARCHAR(100),
	@Telefono NVARCHAR(12),
	@Direccion NVARCHAR(500),
	@CodigoPais NVARCHAR(50),
	@CodigoDocumento NVARCHAR(50),
	@NumeroDocumento NVARCHAR(50)
AS
	UPDATE Cliente
	set Nombre = @Nombre,
	    Apellidos = @Apellidos,
		Email = @Email,
		Telefono = @Telefono,
		Direccion = @Direccion,
		CodigoPais = @CodigoPais,
		CodigoDocumento =@CodigoDocumento,
		NumeroDocumento = @NumeroDocumento
		where ClienteId = @ClienteId;
RETURN 0
