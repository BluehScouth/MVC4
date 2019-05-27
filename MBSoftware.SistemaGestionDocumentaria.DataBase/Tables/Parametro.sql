CREATE TABLE [dbo].[Parametro]
(
	[CodigoParametro] INT NOT NULL PRIMARY KEY,
	[Nombre] NVARCHAR(100) NULL,
	[Descripcion] NVARCHAR(255) NULL,
	[EstadoRegistro] CHAR(1) NULL DEFAULT 'A',
	[UsuarioCreacion] NVARCHAR(50) NULL,
	[FechaCreacion] DATETIME NULL DEFAULT GETDATE(),
	[TerminalCreacion] NVARCHAR(50) NULL,
	[UsuarioModificacion] NVARCHAR(50) NULL,
	[FechaModificacion] DATETIME NULL,
	[TerminalModificacion] NVARCHAR(50) NULL
)
