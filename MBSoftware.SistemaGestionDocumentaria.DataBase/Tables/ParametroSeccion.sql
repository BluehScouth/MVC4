Create Table [Dbo].[ParametroSeccion](
	[CodigoParametro] INT NOT NULL FOREIGN KEY REFERENCES Parametro(CodigoParametro),
	[CodigoSeccion] INT NOT NULL,
	[Nombre] NVARCHAR(100) NULL,
	[SoloLectura] BIT NULL,
	[EstadoRegistro] CHAR(1) NULL DEFAULT 'A',
	[UsuarioCreacion] NVARCHAR(50) NULL,
	[FechaCreacion] DATETIME NULL DEFAULT GETDATE(),
	[TerminalCreacion] NVARCHAR(50) NULL,
	[UsuarioModificacion] NVARCHAR(50) NULL,
	[FechaModificacion] DATETIME NULL,
	[TerminalModificacion] NVARCHAR(50) NULL,
	PRIMARY KEY ([CodigoParametro],[CodigoSeccion]),
	
	)
