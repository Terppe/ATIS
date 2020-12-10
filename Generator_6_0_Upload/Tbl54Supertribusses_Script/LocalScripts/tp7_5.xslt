<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition">    


<xsl:choose>
<xsl:when test="Table ='ControlTemplateButtons++++++++++++++++++'">        
</xsl:when>
<xsl:otherwise>     <![CDATA[      
                 ------- ControlTemplateButtons---------------
    <!--  ]]><xsl:value-of select="BasisTK1"/><![CDATA[-->
    <ControlTemplate x:Key="Search]]><xsl:value-of select="BasissTK1"/><![CDATA[Button" TargetType="Button">   
        <Button
            Width="36"
            Height="36"
            Margin="4"
            Command="{Binding Get]]><xsl:value-of select="BasissTK1"/><![CDATA[ByNameOrIdCommand}"
            Foreground="{DynamicResource MahApps.Brushes.Accent}"
            IsEnabled="True"
            Style="{StaticResource MahApps.Styles.Button.Circle}">
            <Button.ContentTemplate>
                <DataTemplate>
                    <iconPacks:PackIconModern
                        Width="20"
                        Height="20"
                        Kind="PageSearch" />
                </DataTemplate>
            </Button.ContentTemplate>
        </Button>
    </ControlTemplate>

    <ControlTemplate x:Key="Add]]><xsl:value-of select="BasisTK1"/><![CDATA[Button" TargetType="Button">
        <Button
            Width="36"
            Height="36"
            Margin="4"
            Command="{Binding Add]]><xsl:value-of select="BasisTK1"/><![CDATA[Command}"
            Foreground="{DynamicResource MahApps.Brushes.Accent}"
            IsEnabled="True"
            Style="{StaticResource MahApps.Styles.Button.Circle}">
            <Button.ContentTemplate>
                <DataTemplate>
                    <iconPacks:PackIconModern
                        Width="20"
                        Height="20"
                        Kind="Add" />
                </DataTemplate>
            </Button.ContentTemplate>
        </Button>
    </ControlTemplate>
    <ControlTemplate x:Key="Copy]]><xsl:value-of select="BasisTK1"/><![CDATA[Button" TargetType="Button">
        <Button
            Width="36"
            Height="36"
            Margin="4"
            Command="{Binding Copy]]><xsl:value-of select="BasisTK1"/><![CDATA[Command}"
            Foreground="{DynamicResource MahApps.Brushes.Accent}"
            IsEnabled="True"
            Style="{StaticResource MahApps.Styles.Button.Circle}">
            <Button.ContentTemplate>
                <DataTemplate>
                    <iconPacks:PackIconModern
                        Width="20"
                        Height="20"
                        Kind="PageCopy" />
                </DataTemplate>
            </Button.ContentTemplate>
        </Button>
    </ControlTemplate>
    <ControlTemplate x:Key="Save]]><xsl:value-of select="BasisTK1"/><![CDATA[Button" TargetType="Button">
        <Button
            Width="36"
            Height="36"
            Margin="4"
            Command="{Binding Save]]><xsl:value-of select="BasisTK1"/><![CDATA[Command}"
            Foreground="{DynamicResource MahApps.Brushes.Accent}"
            IsEnabled="True"
            Style="{StaticResource MahApps.Styles.Button.Circle}">
            <Button.ContentTemplate>
                <DataTemplate>
                    <iconPacks:PackIconModern
                        Width="20"
                        Height="20"
                        Kind="Save" />
                </DataTemplate>
            </Button.ContentTemplate>
        </Button>
    </ControlTemplate>
    <ControlTemplate x:Key="Delete]]><xsl:value-of select="BasisTK1"/><![CDATA[Button" TargetType="Button">
        <Button
            Width="36"
            Height="36"
            Margin="4"
            Command="{Binding Delete]]><xsl:value-of select="BasisTK1"/><![CDATA[Command}"
            Foreground="{DynamicResource MahApps.Brushes.Accent}"
            IsEnabled="True"
            Style="{StaticResource MahApps.Styles.Button.Circle}">
            <Button.ContentTemplate>
                <DataTemplate>
                    <iconPacks:PackIconModern
                        Width="20"
                        Height="20"
                        Kind="Delete" />
                </DataTemplate>
            </Button.ContentTemplate>
        </Button>
    </ControlTemplate>
	  ]]> 
</xsl:otherwise>    
</xsl:choose>  




</xsl:template>
</xsl:stylesheet>



















