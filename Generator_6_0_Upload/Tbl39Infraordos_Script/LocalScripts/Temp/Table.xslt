<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" 
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
  xmlns:xs="http://www.w3.org/2001/XMLSchema"   
  xmlns:fn="http://www.w3.org/2005/xpath-functions">
    <xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
    <xsl:template match="Definition">USE [<xsl:value-of select="Version"/>]
GO
/****** Object:  Table [dbo].[<xsl:value-of select="Table"/>]    Script Date:  <xsl:value-of select="DateTime"/>   ******/  
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[<xsl:value-of select="Table"/>] (
            <xsl:for-each select="//Fields/Field">
            <xsl:choose>
            <xsl:when test="Type='uniqueidentifier'"><!-- uniqueidentifier -->[<xsl:value-of select="Name"/>] [uniqueidentifier]  <xsl:value-of select="NotNull"/>,
            </xsl:when>
            <xsl:when test="Type='String'"><!-- String -->[<xsl:value-of select="Name"/>] [nvarchar] (<xsl:value-of select="Length"/>) <xsl:value-of select="Collate"/> <xsl:value-of select="NotNull"/>,
            </xsl:when>
            <xsl:when test="Type='nvarchar'"><!-- nvarchar() -->[<xsl:value-of select="Name"/>] [nvarchar] (<xsl:value-of select="Length"/>) <xsl:value-of select="Collate"/> <xsl:value-of select="NotNull"/>,
            </xsl:when>
            <xsl:when test="Type='varchar'"><!-- varchar() -->[<xsl:value-of select="Name"/>] [varchar] (<xsl:value-of select="Length"/>) <xsl:value-of select="Collate"/> <xsl:value-of select="NotNull"/>,
            </xsl:when>
            <xsl:when test="Type='bigint'"><!-- bigint -->[<xsl:value-of select="Name"/>] [bigint] <xsl:value-of select="NotNull"/>,
            </xsl:when>
            <xsl:when test="Type='int'"><!-- int -->[<xsl:value-of select="Name"/>] [int] <xsl:value-of select="NotNull"/>,
            </xsl:when>
            <xsl:when test="Type='tinyint'"><!-- tinyint -->[<xsl:value-of select="Name"/>] [tinyint] <xsl:value-of select="NotNull"/>,
            </xsl:when>
            <xsl:when test="Type='smallint'"><!-- smallint -->[<xsl:value-of select="Name"/>] [smallint] <xsl:value-of select="NotNull"/>,       
            </xsl:when>
            <xsl:when test="Type='float'"><!-- float -->[<xsl:value-of select="Name"/>] [float] <xsl:value-of select="NotNull"/>,
            </xsl:when>
            <xsl:when test="Type='decimal'"><!-- decimal -->[<xsl:value-of select="Name"/>] [decimal](<xsl:value-of select="Length"/>) <xsl:value-of select="NotNull"/>,
            </xsl:when>
            <xsl:when test="Type='bit'"><!-- bit -->[<xsl:value-of select="Name"/>] [bit] <xsl:value-of select="NotNull"/>,
            </xsl:when>
            <xsl:when test="Type='geography'"><!-- geography -->[<xsl:value-of select="Name"/>] [geography] <xsl:value-of select="NotNull"/>,
            </xsl:when>
            <xsl:when test="Type='geometry'"><!-- geometry -->[<xsl:value-of select="Name"/>] [geometry] <xsl:value-of select="NotNull"/>,
            </xsl:when>
            <xsl:when test="Type='hierarchyid'"><!-- hierarchyid -->[<xsl:value-of select="Name"/>] [hierarchyid] <xsl:value-of select="NotNull"/>,
            </xsl:when>
            <xsl:when test="Type='smalldatetime'"><!-- smalldatetime -->[<xsl:value-of select="Name"/>] [smalldatetime] <xsl:value-of select="NotNull"/>,
            </xsl:when>
            <xsl:when test="Type='datetime'"><!-- datetime -->[<xsl:value-of select="Name"/>] [datetime] <xsl:value-of select="NotNull" />,
            </xsl:when>
            <xsl:when test="Type='datetime2'"><!-- datetime2 -->[<xsl:value-of select="Name"/>] [datetime2] (<xsl:value-of select="Length"/>) <xsl:value-of select="NotNull"/>,
            </xsl:when>
            <xsl:when test="Type='datetimeoffset'"><!-- datetimeoffset -->[<xsl:value-of select="Name"/>] [datetimeoffset] (<xsl:value-of select="Length"/>) <xsl:value-of select="NotNull"/>,
            </xsl:when>
            <xsl:when test="Type='date'"><!-- date -->[<xsl:value-of select="Name"/>] [date] <xsl:value-of select="NotNull"/>,
            </xsl:when>
            <xsl:when test="Type='time'"><!-- time -->[<xsl:value-of select="Name"/>] [time] (<xsl:value-of select="Length"/>) <xsl:value-of select="NotNull"/>,
            </xsl:when>
            <xsl:when test="Type='ntext'"><!-- ntext -->[<xsl:value-of select="Name"/>] [ntext] <xsl:value-of select="Collate"/> <xsl:value-of select="NotNull"/>,
            </xsl:when>
            <xsl:when test="Type='timestamp'"><!-- timestamp -->[<xsl:value-of select="Name"/>] [timestamp] <xsl:value-of select="NotNull"/>,
            </xsl:when>
            <xsl:when test="Type='varbinary'"><!-- varbinary -->[<xsl:value-of select="Name"/>] [varbinary] (<xsl:value-of select="Length"/>) <xsl:value-of select="Collate" /> <xsl:value-of select="NotNull"/>,
            </xsl:when>
            <xsl:when test="Type='UNIQUEIDENTIFIER'"><!-- UNIQUEIDENTIFIER -->[<xsl:value-of select="Name"/>] [UNIQUEIDENTIFIER]  <xsl:value-of select="Collate"/> <xsl:value-of select="NotNull"/>,
            </xsl:when>
          </xsl:choose>
         </xsl:for-each>
<xsl:if test="KeyPK !='NULL'">
CONSTRAINT [<xsl:value-of select="KeyPK"/>] PRIMARY KEY CLUSTERED 
(
	<xsl:value-of select="Primary"/> 
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
</xsl:if> 
<xsl:if test="KeyIX4 !='NULL'"> , 
CONSTRAINT [<xsl:value-of select="KeyIX4"/>] UNIQUE NONCLUSTERED 
(
	<xsl:value-of select="Unique4"/>
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
</xsl:if> 
<xsl:if test="KeyIX3 !='NULL'"> , 
CONSTRAINT [<xsl:value-of select="KeyIX3"/>] UNIQUE NONCLUSTERED 
(
	<xsl:value-of select="Unique3"/>
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
</xsl:if> 
<xsl:if test="KeyIX2 !='NULL'"> , 
CONSTRAINT [<xsl:value-of select="KeyIX2"/>] UNIQUE NONCLUSTERED 
(
	<xsl:value-of select="Unique2"/>
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
</xsl:if> 
<xsl:if test="KeyIX1 !='NULL'"> , 
CONSTRAINT [<xsl:value-of select="KeyIX1"/>] UNIQUE NONCLUSTERED 
(
	<xsl:value-of select="Unique1"/>
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE = OFF,  IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,  ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
</xsl:if> 
) ON [PRIMARY] 

GO

SET ANSI_PADDING OFF
GO

<xsl:choose>
<xsl:when test="Table ='Valid+++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='TblCountries'">   
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">   
</xsl:when>
<xsl:otherwise>  
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>] ADD  CONSTRAINT [DF_<xsl:value-of select="Table"/>_Valid]  DEFAULT ((0)) FOR [Valid]
GO
</xsl:otherwise>    
</xsl:choose> 

<xsl:if test="KeyFK1 !='NULL'">
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>]  WITH CHECK ADD  CONSTRAINT 
[<xsl:value-of select="KeyFK1"/>] FOREIGN KEY ([<xsl:value-of select="Foreign1"/>])
REFERENCES [dbo].[<xsl:value-of select="TableFK1"/>] ([<xsl:value-of select="Foreign1"/>])
GO
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>] CHECK CONSTRAINT [<xsl:value-of select="KeyFK1"/>]
GO
</xsl:if> 
<xsl:if test="KeyFK2 !='NULL'">
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>]  WITH CHECK ADD  CONSTRAINT 
[<xsl:value-of select="KeyFK2"/>] FOREIGN KEY ([<xsl:value-of select="Foreign2"/>])
REFERENCES [dbo].[<xsl:value-of select="TableFK2"/>] ([<xsl:value-of select="Foreign2"/>])
GO
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>] CHECK CONSTRAINT [<xsl:value-of select="KeyFK2"/>]
GO
</xsl:if> 
<xsl:if test="KeyFK3 !='NULL'">
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>]  WITH CHECK ADD  CONSTRAINT 
[<xsl:value-of select="KeyFK3"/>] FOREIGN KEY ([<xsl:value-of select="Foreign3"/>])
REFERENCES [dbo].[<xsl:value-of select="TableFK3"/>] ([<xsl:value-of select="Foreign3"/>])
GO
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>] CHECK CONSTRAINT [<xsl:value-of select="KeyFK3"/>]
GO
</xsl:if> 
<xsl:if test="KeyFK4 !='NULL'">
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>]  WITH CHECK ADD  CONSTRAINT 
[<xsl:value-of select="KeyFK4"/>] FOREIGN KEY ([<xsl:value-of select="Foreign4"/>])
REFERENCES [dbo].[<xsl:value-of select="TableFK4"/>] ([<xsl:value-of select="Foreign4"/>])
GO
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>] CHECK CONSTRAINT [<xsl:value-of select="KeyFK4"/>]
GO
</xsl:if> 
<xsl:if test="KeyFK5 !='NULL'">
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>]  WITH CHECK ADD  CONSTRAINT 
[<xsl:value-of select="KeyFK5"/>] FOREIGN KEY ([<xsl:value-of select="Foreign5"/>])
REFERENCES [dbo].[<xsl:value-of select="TableFK5"/>] ([<xsl:value-of select="Foreign5"/>])
GO
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>] CHECK CONSTRAINT [<xsl:value-of select="KeyFK5"/>]
GO
</xsl:if> 
<xsl:if test="KeyFK6 !='NULL'">
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>]  WITH CHECK ADD  CONSTRAINT 
[<xsl:value-of select="KeyFK6"/>] FOREIGN KEY ([<xsl:value-of select="Foreign6"/>])
REFERENCES [dbo].[<xsl:value-of select="TableFK6"/>] ([<xsl:value-of select="Foreign6"/>])
GO
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>] CHECK CONSTRAINT [<xsl:value-of select="KeyFK6"/>]
GO
</xsl:if> 
<xsl:if test="KeyFK7 !='NULL'">
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>]  WITH CHECK ADD  CONSTRAINT 
[<xsl:value-of select="KeyFK7"/>] FOREIGN KEY ([<xsl:value-of select="Foreign7"/>])
REFERENCES [dbo].[<xsl:value-of select="TableFK7"/>] ([<xsl:value-of select="Foreign7"/>])
GO
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>] CHECK CONSTRAINT [<xsl:value-of select="KeyFK7"/>]
GO
</xsl:if> 
<xsl:if test="KeyFK8 !='NULL'">
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>]  WITH CHECK ADD  CONSTRAINT 
[<xsl:value-of select="KeyFK8"/>] FOREIGN KEY ([<xsl:value-of select="Foreign8"/>])
REFERENCES [dbo].[<xsl:value-of select="TableFK8"/>] ([<xsl:value-of select="Foreign8"/>])
GO
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>] CHECK CONSTRAINT [<xsl:value-of select="KeyFK8"/>]
GO
</xsl:if> 
<xsl:if test="KeyFK9 !='NULL'">
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>]  WITH CHECK ADD  CONSTRAINT 
[<xsl:value-of select="KeyFK9"/>] FOREIGN KEY ([<xsl:value-of select="Foreign9"/>])
REFERENCES [dbo].[<xsl:value-of select="TableFK9"/>] ([<xsl:value-of select="Foreign9"/>])
GO
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>] CHECK CONSTRAINT [<xsl:value-of select="KeyFK9"/>]
GO
</xsl:if> 
<xsl:if test="KeyFK10 !='NULL'">
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>]  WITH CHECK ADD  CONSTRAINT 
[<xsl:value-of select="KeyFK10"/>] FOREIGN KEY ([<xsl:value-of select="Foreign10"/>])
REFERENCES [dbo].[<xsl:value-of select="TableFK10"/>] ([<xsl:value-of select="Foreign10"/>])
GO
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>] CHECK CONSTRAINT [<xsl:value-of select="KeyFK10"/>]
GO
</xsl:if> 
<xsl:if test="KeyFK11 !='NULL'">
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>]  WITH CHECK ADD  CONSTRAINT 
[<xsl:value-of select="KeyFK11"/>] FOREIGN KEY ([<xsl:value-of select="Foreign11"/>])
REFERENCES [dbo].[<xsl:value-of select="TableFK11"/>] ([<xsl:value-of select="Foreign11"/>])
GO
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>] CHECK CONSTRAINT [<xsl:value-of select="KeyFK11"/>]
GO
</xsl:if> 
<xsl:if test="KeyFK12 !='NULL'">
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>]  WITH CHECK ADD  CONSTRAINT 
[<xsl:value-of select="KeyFK12"/>] FOREIGN KEY ([<xsl:value-of select="Foreign12"/>])
REFERENCES [dbo].[<xsl:value-of select="TableFK12"/>] ([<xsl:value-of select="Foreign12"/>])
GO
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>] CHECK CONSTRAINT [<xsl:value-of select="KeyFK12"/>]
GO
</xsl:if> 
<xsl:if test="KeyFK13 !='NULL'">
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>]  WITH CHECK ADD  CONSTRAINT 
[<xsl:value-of select="KeyFK13"/>] FOREIGN KEY ([<xsl:value-of select="Foreign13"/>])
REFERENCES [dbo].[<xsl:value-of select="TableFK13"/>] ([<xsl:value-of select="Foreign13"/>])
GO
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>] CHECK CONSTRAINT [<xsl:value-of select="KeyFK13"/>]
GO
</xsl:if> 
<xsl:if test="KeyFK14 !='NULL'">
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>]  WITH CHECK ADD  CONSTRAINT 
[<xsl:value-of select="KeyFK14"/>] FOREIGN KEY ([<xsl:value-of select="Foreign14"/>])
REFERENCES [dbo].[<xsl:value-of select="TableFK14"/>] ([<xsl:value-of select="Foreign14"/>])
GO
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>] CHECK CONSTRAINT [<xsl:value-of select="KeyFK14"/>]
GO
</xsl:if> 
<xsl:if test="KeyFK15 !='NULL'">
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>]  WITH CHECK ADD  CONSTRAINT 
[<xsl:value-of select="KeyFK15"/>] FOREIGN KEY ([<xsl:value-of select="Foreign15"/>])
REFERENCES [dbo].[<xsl:value-of select="TableFK15"/>] ([<xsl:value-of select="Foreign15"/>])
GO
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>] CHECK CONSTRAINT [<xsl:value-of select="KeyFK15"/>]
GO
</xsl:if> 
<xsl:if test="KeyFK16 !='NULL'">
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>]  WITH CHECK ADD  CONSTRAINT 
[<xsl:value-of select="KeyFK16"/>] FOREIGN KEY ([<xsl:value-of select="Foreign16"/>])
REFERENCES [dbo].[<xsl:value-of select="TableFK16"/>] ([<xsl:value-of select="Foreign16"/>])
GO
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>] CHECK CONSTRAINT [<xsl:value-of select="KeyFK16"/>]
GO
</xsl:if> 
<xsl:if test="KeyFK17 !='NULL'">
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>]  WITH CHECK ADD  CONSTRAINT 
[<xsl:value-of select="KeyFK17"/>] FOREIGN KEY ([<xsl:value-of select="Foreign17"/>])
REFERENCES [dbo].[<xsl:value-of select="TableFK17"/>] ([<xsl:value-of select="Foreign17"/>])
GO
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>] CHECK CONSTRAINT [<xsl:value-of select="KeyFK17"/>]
GO
</xsl:if> 
<xsl:if test="KeyFK18 !='NULL'">
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>]  WITH CHECK ADD  CONSTRAINT 
[<xsl:value-of select="KeyFK18"/>] FOREIGN KEY ([<xsl:value-of select="Foreign18"/>])
REFERENCES [dbo].[<xsl:value-of select="TableFK18"/>] ([<xsl:value-of select="Foreign18"/>])
GO
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>] CHECK CONSTRAINT [<xsl:value-of select="KeyFK18"/>]
GO
</xsl:if> 
<xsl:if test="KeyFK19 !='NULL'">
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>]  WITH CHECK ADD  CONSTRAINT 
[<xsl:value-of select="KeyFK19"/>] FOREIGN KEY ([<xsl:value-of select="Foreign19"/>])
REFERENCES [dbo].[<xsl:value-of select="TableFK19"/>] ([<xsl:value-of select="Foreign19"/>])
GO
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>] CHECK CONSTRAINT [<xsl:value-of select="KeyFK19"/>]
GO
</xsl:if> 
<xsl:if test="KeyFK20 !='NULL'">
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>]  WITH CHECK ADD  CONSTRAINT 
[<xsl:value-of select="KeyFK20"/>] FOREIGN KEY ([<xsl:value-of select="Foreign20"/>])
REFERENCES [dbo].[<xsl:value-of select="TableFK20"/>] ([<xsl:value-of select="Foreign20"/>])
GO
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>] CHECK CONSTRAINT [<xsl:value-of select="KeyFK20"/>]
GO
</xsl:if> 
<xsl:if test="KeyFK21 !='NULL'">
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>]  WITH CHECK ADD  CONSTRAINT 
[<xsl:value-of select="KeyFK21"/>] FOREIGN KEY ([<xsl:value-of select="Foreign21"/>])
REFERENCES [dbo].[<xsl:value-of select="TableFK21"/>] ([<xsl:value-of select="Foreign21"/>])
GO
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>] CHECK CONSTRAINT [<xsl:value-of select="KeyFK21"/>]
GO
</xsl:if> 
<xsl:if test="KeyFK22 !='NULL'">
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>]  WITH CHECK ADD  CONSTRAINT 
[<xsl:value-of select="KeyFK22"/>] FOREIGN KEY ([<xsl:value-of select="Foreign22"/>])
REFERENCES [dbo].[<xsl:value-of select="TableFK22"/>] ([<xsl:value-of select="Foreign22"/>])
GO
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>] CHECK CONSTRAINT [<xsl:value-of select="KeyFK22"/>]
GO
</xsl:if> 
<xsl:if test="KeyFK23 !='NULL'">
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>]  WITH CHECK ADD  CONSTRAINT 
[<xsl:value-of select="KeyFK23"/>] FOREIGN KEY ([<xsl:value-of select="Foreign23"/>])
REFERENCES [dbo].[<xsl:value-of select="TableFK23"/>] ([<xsl:value-of select="Foreign23"/>])
GO
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>] CHECK CONSTRAINT [<xsl:value-of select="KeyFK23"/>]
GO
</xsl:if> 
<xsl:if test="KeyFK24 !='NULL'">
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>]  WITH CHECK ADD  CONSTRAINT 
[<xsl:value-of select="KeyFK24"/>] FOREIGN KEY ([<xsl:value-of select="Foreign24"/>])
REFERENCES [dbo].[<xsl:value-of select="TableFK24"/>] ([<xsl:value-of select="Foreign24"/>])
GO
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>] CHECK CONSTRAINT [<xsl:value-of select="KeyFK24"/>]
GO
</xsl:if> 
<xsl:if test="KeyFK25 !='NULL'">
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>]  WITH CHECK ADD  CONSTRAINT 
[<xsl:value-of select="KeyFK25"/>] FOREIGN KEY ([<xsl:value-of select="Foreign25"/>])
REFERENCES [dbo].[<xsl:value-of select="TableFK25"/>] ([<xsl:value-of select="Foreign25"/>])
GO
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>] CHECK CONSTRAINT [<xsl:value-of select="KeyFK25"/>]
GO
</xsl:if> 
<xsl:if test="KeyFK26 !='NULL'">
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>]  WITH CHECK ADD  CONSTRAINT 
[<xsl:value-of select="KeyFK26"/>] FOREIGN KEY ([<xsl:value-of select="Foreign26"/>])
REFERENCES [dbo].[<xsl:value-of select="TableFK26"/>] ([<xsl:value-of select="Foreign26"/>])
GO
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>] CHECK CONSTRAINT [<xsl:value-of select="KeyFK26"/>]
GO
</xsl:if> 
<xsl:if test="KeyFK27 !='NULL'">
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>]  WITH CHECK ADD  CONSTRAINT 
[<xsl:value-of select="KeyFK27"/>] FOREIGN KEY ([<xsl:value-of select="Foreign27"/>])
REFERENCES [dbo].[<xsl:value-of select="TableFK27"/>] ([<xsl:value-of select="Foreign27"/>])
GO
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>] CHECK CONSTRAINT [<xsl:value-of select="KeyFK27"/>]
GO
</xsl:if> 


<xsl:if test="KeyDF1 !='NULL'">
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>]  ADD  CONSTRAINT 
[<xsl:value-of select="KeyDF1"/>] DEFAULT <xsl:value-of select="Default1"/>
GO
</xsl:if> 

<xsl:if test="KeyDF2 !='NULL'">
ALTER TABLE [dbo].[<xsl:value-of select="Table"/>]  ADD  CONSTRAINT 
[<xsl:value-of select="KeyDF2"/>] DEFAULT <xsl:value-of select="Default2"/>
GO
</xsl:if> 
  </xsl:template>
</xsl:stylesheet>
