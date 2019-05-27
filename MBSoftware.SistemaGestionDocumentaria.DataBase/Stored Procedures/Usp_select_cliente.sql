CREATE PROCEDURE [dbo].[Usp_select_cliente]
AS
BEGIN
	SELECT  ClienteId,Nombre,Apellidos,Email,Telefono,Direccion FROM Cliente
END

