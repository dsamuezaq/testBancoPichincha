USE [BP_TEST]
GO
/****** Object:  Table [dbo].[bp_cliente]    Script Date: 28/02/2022 18:46:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bp_cliente](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[bg_per_id] [int] NOT NULL,
	[bp_cli_password] [varchar](400) NULL,
	[bp_cli_estado] [char](1) NULL,
 CONSTRAINT [PK__bp_cliente] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[bp_cuenta]    Script Date: 28/02/2022 18:46:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bp_cuenta](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[bg_per_id] [int] NOT NULL,
	[bp_cue_numero] [int] NULL,
	[bp_cue_tipo] [char](50) NULL,
	[bp_cue_sal_inicial] [decimal](8, 4) NULL,
	[bp_cue_estado] [char](1) NULL,
 CONSTRAINT [PK__bp_cuenta] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[bp_movimiento]    Script Date: 28/02/2022 18:46:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bp_movimiento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[bg_cue_id] [int] NOT NULL,
	[Fecha] [datetime] NULL,
	[bp_mov_tipo] [varchar](50) NULL,
	[bp_mov_valor] [decimal](8, 4) NULL,
	[bp_mov_saldo] [decimal](8, 4) NULL,
 CONSTRAINT [PK__bp_movimiento] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[bp_persona]    Script Date: 28/02/2022 18:46:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bp_persona](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[bp_per_nombre] [varchar](400) NULL,
	[bp_per_genero] [char](1) NULL,
	[bp_per_identificacion] [varchar](13) NULL,
	[bp_per_direccion] [varchar](1000) NULL,
	[bp_per_telefono] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[bp_movimiento] ADD  DEFAULT (getdate()) FOR [Fecha]
GO
ALTER TABLE [dbo].[bp_cliente]  WITH CHECK ADD FOREIGN KEY([bg_per_id])
REFERENCES [dbo].[bp_persona] ([Id])
GO
ALTER TABLE [dbo].[bp_cuenta]  WITH CHECK ADD FOREIGN KEY([bg_per_id])
REFERENCES [dbo].[bp_persona] ([Id])
GO
ALTER TABLE [dbo].[bp_movimiento]  WITH CHECK ADD FOREIGN KEY([bg_cue_id])
REFERENCES [dbo].[bp_cuenta] ([Id])
GO
