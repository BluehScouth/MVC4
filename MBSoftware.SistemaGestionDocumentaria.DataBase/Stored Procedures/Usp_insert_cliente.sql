CREATE PROCEDURE [dbo].[Usp_insert_cliente]
    @ClienteId BIGINT OUTPUT,
	@Nombre NVARCHAR(100),
	@Apellidos NVARCHAR(100),
	@Email NVARCHAR(100),
	@Telefono NVARCHAR(12),
	@Direccion NVARCHAR(500),
	@CodigoPais NVARCHAR(50),
	@CodigoDocumento NVARCHAR(50),
	@NumeroDocumento NVARCHAR(50)
AS
Begin
	INSERT INTO Cliente(Nombre,Apellidos,Email,Telefono,Direccion,CodigoPais,CodigoDocumento,NumeroDocumento)
	values (@Nombre,@Apellidos,@Email,@Telefono,@Direccion,@CodigoPais,@CodigoDocumento,@NumeroDocumento)
	
	SET @ClienteId= SCOPE_IDENTITY()
End	



