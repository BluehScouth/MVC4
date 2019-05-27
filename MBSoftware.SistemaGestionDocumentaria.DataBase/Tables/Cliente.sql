CREATE TABLE [dbo].[Cliente]
(
	[ClienteId] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Nombre] NVARCHAR(100),
	[Apellidos] NVARCHAR(100),
	[Email] NVARCHAR(100),
    [Telefono] NVARCHAR(100),
	[Direccion] NVARCHAR(500),
	[CodigoPais] NVARCHAR (50),
	[CodigoDocumento] NVARCHAR(50),
	[NumeroDocumento] NVARCHAR(50),
	[EstadoRegistro] CHAR(1) NULL DEFAULT 'A',
	[UsuarioCreacion] NVARCHAR(50) NULL,
	[FechaCreacion] DATETIME NULL DEFAULT GETDATE(),
	[TerminalCreacion] NVARCHAR(50) NULL,
	[UsuarioModificacion] NVARCHAR(50) NULL,
	[FechaModificacion] DATETIME NULL,
	[TerminalModificacion] NVARCHAR(50) NULL
)

