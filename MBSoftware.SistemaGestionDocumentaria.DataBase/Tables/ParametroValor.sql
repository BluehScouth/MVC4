CREATE TABLE [dbo].[ParametroValor](
	[CodigoParametro] INT NOT NULL, 
	[CodigoSeccion] INT NOT NULL,
	[CodigoValor] INT NOT NULL,
	[Valor] NVARCHAR(MAX) NULL,
	[EstadoRegistro] CHAR(1) NULL DEFAULT 'A',
	[UsuarioCreacion] NVARCHAR(50) NULL,
	[FechaCreacion] DATETIME NULL DEFAULT GETDATE(),
	[TerminalCreacion] NVARCHAR(50) NULL,
	[UsuarioModificacion] NVARCHAR(50) NULL,
	[FechaModificacion] DATETIME NULL,
	[TerminalModificacion] NVARCHAR(50) NULL
) 