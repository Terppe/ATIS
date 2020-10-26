<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition"><![CDATA[<?xml version="1.0" encoding="utf-8"?>
<root>
	<!-- 
    Microsoft ResX Schema 
    
    Version 2.0
    
    The primary goals of this format is to allow a simple XML format 
    that is mostly human readable. The generation and parsing of the 
    various data types are done through the TypeConverter classes 
    associated with the data types.
    
    Example:
    
    ... ado.net/XML headers & schema ...
    <resheader name="resmimetype">text/microsoft-resx</resheader>
    <resheader name="version">2.0</resheader>
    <resheader name="reader">System.Resources.ResXResourceReader, System.Windows.Forms, ...</resheader>
    <resheader name="writer">System.Resources.ResXResourceWriter, System.Windows.Forms, ...</resheader>
    <data name="Name1"><value>this is my long string</value><comment>this is a comment</comment></data>
    <data name="Color1" type="System.Drawing.Color, System.Drawing">Blue</data>
    <data name="Bitmap1" mimetype="application/x-microsoft.net.object.binary.base64">
        <value>[base64 mime encoded serialized .NET Framework object]</value>
    </data>
    <data name="Icon1" type="System.Drawing.Icon, System.Drawing" mimetype="application/x-microsoft.net.object.bytearray.base64">
        <value>[base64 mime encoded string representing a byte array form of the .NET Framework object]</value>
        <comment>This is a comment</comment>
    </data>
                
    There are any number of "resheader" rows that contain simple 
    name/value pairs.
    
    Each data row contains a name, and value. The row also contains a 
    type or mimetype. Type corresponds to a .NET class that support 
    text/value conversion through the TypeConverter architecture. 
    Classes that don't support this are serialized and stored with the 
    mimetype set.
    
    The mimetype is used for serialized objects, and tells the 
    ResXResourceReader how to depersist the object. This is currently not 
    extensible. For a given mimetype the value must be set accordingly:
    
    Note - application/x-microsoft.net.object.binary.base64 is the format 
    that the ResXResourceWriter will generate, however the reader can 
    read any of the formats listed below.
    
    mimetype: application/x-microsoft.net.object.binary.base64
    value   : The object must be serialized with 
            : System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
            : and then encoded with base64 encoding.
    
    mimetype: application/x-microsoft.net.object.soap.base64
    value   : The object must be serialized with 
            : System.Runtime.Serialization.Formatters.Soap.SoapFormatter
            : and then encoded with base64 encoding.

    mimetype: application/x-microsoft.net.object.bytearray.base64
    value   : The object must be serialized into a byte array 
            : using a System.ComponentModel.TypeConverter
            : and then encoded with base64 encoding.
    -->
	<xsd:schema id="root" xmlns="" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
		<xsd:import namespace="http://www.w3.org/XML/1998/namespace"/>
		<xsd:element name="root" msdata:IsDataSet="true">
			<xsd:complexType>
				<xsd:choice maxOccurs="unbounded">
					<xsd:element name="metadata">
						<xsd:complexType>
							<xsd:sequence>
								<xsd:element name="value" type="xsd:string" minOccurs="0"/>
							</xsd:sequence>
							<xsd:attribute name="name" use="required" type="xsd:string"/>
							<xsd:attribute name="type" type="xsd:string"/>
							<xsd:attribute name="mimetype" type="xsd:string"/>
							<xsd:attribute ref="xml:space"/>
						</xsd:complexType>
					</xsd:element>
					<xsd:element name="assembly">
						<xsd:complexType>
							<xsd:attribute name="alias" type="xsd:string"/>
							<xsd:attribute name="name" type="xsd:string"/>
						</xsd:complexType>
					</xsd:element>
					<xsd:element name="data">
						<xsd:complexType>
							<xsd:sequence>
								<xsd:element name="value" type="xsd:string" minOccurs="0" msdata:Ordinal="1"/>
								<xsd:element name="comment" type="xsd:string" minOccurs="0" msdata:Ordinal="2"/>
							</xsd:sequence>
							<xsd:attribute name="name" type="xsd:string" use="required" msdata:Ordinal="1"/>
							<xsd:attribute name="type" type="xsd:string" msdata:Ordinal="3"/>
							<xsd:attribute name="mimetype" type="xsd:string" msdata:Ordinal="4"/>
							<xsd:attribute ref="xml:space"/>
						</xsd:complexType>
					</xsd:element>
					<xsd:element name="resheader">
						<xsd:complexType>
							<xsd:sequence>
								<xsd:element name="value" type="xsd:string" minOccurs="0" msdata:Ordinal="1"/>
							</xsd:sequence>
							<xsd:attribute name="name" type="xsd:string" use="required"/>
						</xsd:complexType>
					</xsd:element>
				</xsd:choice>
			</xsd:complexType>
		</xsd:element>
	</xsd:schema>
	<resheader name="resmimetype">
		<value>text/microsoft-resx</value>
	</resheader>
	<resheader name="version">
		<value>2.0</value>
	</resheader>
	<resheader name="reader">
		<value>System.Resources.ResXResourceReader, System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>
	</resheader>
	<resheader name="writer">
		<value>System.Resources.ResXResourceWriter, System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>
	</resheader>]]>
<!-- Anfang Schleife für alle +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ -->
<xsl:for-each select="//Fields/Field">
    <![CDATA[<data name="dv]]><xsl:value-of select="Name"/><![CDATA[" xml:space="preserve"><value>]]><xsl:value-of select="Name"/><![CDATA[</value></data>  ]]>
</xsl:for-each>
<!-- Ende Schleife  für alle ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ -->
          
<!-- Anfang View +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ -->
<xsl:if test="TableTK1 !='NULL'">
    <![CDATA[<data name="dvView]]><xsl:value-of select="TableTK1"/><![CDATA[" xml:space="preserve"> <value>View </value></data>
    <data name="dvViewLink]]><xsl:value-of select="TableTK1"/><![CDATA[" xml:space="preserve"> <value>]]><xsl:value-of select="TableTK1"/><![CDATA[</value></data>]]>
</xsl:if> 

<xsl:if test="TableFK1 !='NULL'">
    <![CDATA[<data name="dvView]]><xsl:value-of select="TableFK1"/><![CDATA[" xml:space="preserve"><value>View </value></data>
    <data name="dvViewLink]]><xsl:value-of select="TableFK1"/><![CDATA[" xml:space="preserve"><value>]]><xsl:value-of select="TableFK1"/><![CDATA[</value></data>]]>
</xsl:if>
 
<xsl:if test="TableFK2 !='NULL'">
    <![CDATA[<data name="dvView]]><xsl:value-of select="TableFK2"/><![CDATA[" xml:space="preserve"><value>View </value></data>
    <data name="dvViewLink]]><xsl:value-of select="TableFK2"/><![CDATA[" xml:space="preserve"><value>]]><xsl:value-of select="TableFK2"/><![CDATA[</value></data>]]>
</xsl:if> 
<xsl:if test="TableBK1 !='NULL'">
    <![CDATA[<data name="dvView]]><xsl:value-of select="TableBK1"/><![CDATA[" xml:space="preserve"> <value>View </value></data>
    <data name="dvViewLink]]><xsl:value-of select="TableBK1"/><![CDATA[" xml:space="preserve"> <value>]]><xsl:value-of select="TableBK1"/><![CDATA[</value></data>]]>
</xsl:if> 
<xsl:if test="TableBK2 !='NULL'">
    <![CDATA[<data name="dvView]]><xsl:value-of select="TableBK2"/><![CDATA[" xml:space="preserve"> <value>View </value></data>
    <data name="dvViewLink]]><xsl:value-of select="TableBK2"/><![CDATA[" xml:space="preserve"> <value>]]><xsl:value-of select="TableBK2"/><![CDATA[</value></data>]]>
</xsl:if> 
<xsl:if test="TableBK3 !='NULL'">
    <![CDATA[<data name="dvView]]><xsl:value-of select="TableBK3"/><![CDATA[" xml:space="preserve"> <value>View </value></data>
    <data name="dvViewLink]]><xsl:value-of select="TableBK3"/><![CDATA[" xml:space="preserve"> <value>]]><xsl:value-of select="TableBK3"/><![CDATA[</value></data>]]>
</xsl:if> 
<xsl:if test="TableBK4 !='NULL'">
    <![CDATA[<data name="dvView]]><xsl:value-of select="TableBK4"/><![CDATA[" xml:space="preserve"> <value>View </value></data>
    <data name="dvViewLink]]><xsl:value-of select="TableBK4"/><![CDATA[" xml:space="preserve"> <value>]]><xsl:value-of select="TableBK4"/><![CDATA[</value></data>]]>
</xsl:if> 
<xsl:if test="TableBK5 !='NULL'">
    <![CDATA[<data name="dvView]]><xsl:value-of select="TableBK5"/><![CDATA[" xml:space="preserve"> <value>View </value></data>
    <data name="dvViewLink]]><xsl:value-of select="TableBK5"/><![CDATA[" xml:space="preserve"> <value>]]><xsl:value-of select="TableBK5"/><![CDATA[</value></data>]]>
</xsl:if> 
<xsl:if test="TableBK6 !='NULL'">
    <![CDATA[<data name="dvView]]><xsl:value-of select="TableBK6"/><![CDATA[" xml:space="preserve"> <value>View </value></data>
    <data name="dvViewLink]]><xsl:value-of select="TableBK6"/><![CDATA[" xml:space="preserve"> <value>]]><xsl:value-of select="TableBK6"/><![CDATA[</value></data>]]>
</xsl:if> 
<xsl:if test="TableBK7 !='NULL'">
    <![CDATA[<data name="dvView]]><xsl:value-of select="TableBK7"/><![CDATA[" xml:space="preserve"> <value>View </value></data>
    <data name="dvViewLink]]><xsl:value-of select="TableBK7"/><![CDATA[" xml:space="preserve"> <value>]]><xsl:value-of select="TableBK7"/><![CDATA[</value></data>]]>
</xsl:if> 
<xsl:if test="TableBK8 !='NULL'">
    <![CDATA[<data name="dvView]]><xsl:value-of select="TableBK8"/><![CDATA[" xml:space="preserve"> <value>View </value></data>
    <data name="dvViewLink]]><xsl:value-of select="TableBK8"/><![CDATA[" xml:space="preserve"> <value>]]><xsl:value-of select="TableBK8"/><![CDATA[</value></data>]]>
</xsl:if> 
<xsl:if test="TableBK9 !='NULL'">
    <![CDATA[<data name="dvView]]><xsl:value-of select="TableBK9"/><![CDATA[" xml:space="preserve"> <value>View </value></data>
    <data name="dvViewLink]]><xsl:value-of select="TableBK9"/><![CDATA[" xml:space="preserve"> <value>]]><xsl:value-of select="TableBK9"/><![CDATA[</value></data>]]>
</xsl:if> 
<xsl:if test="TableBK10 !='NULL'">
    <![CDATA[<data name="dvView]]><xsl:value-of select="TableBK10"/><![CDATA[" xml:space="preserve"> <value>View </value></data>
    <data name="dvViewLink]]><xsl:value-of select="TableBK10"/><![CDATA[" xml:space="preserve"> <value>]]><xsl:value-of select="TableBK10"/><![CDATA[</value></data>]]>
</xsl:if> 
<xsl:if test="TableBK11 !='NULL'">
    <![CDATA[<data name="dvView]]><xsl:value-of select="TableBK11"/><![CDATA[" xml:space="preserve"> <value>View </value></data>
    <data name="dvViewLink]]><xsl:value-of select="TableBK11"/><![CDATA[" xml:space="preserve"> <value>]]><xsl:value-of select="TableBK11"/><![CDATA[</value></data>]]>
</xsl:if> 
<xsl:if test="TableBK12 !='NULL'">
    <![CDATA[<data name="dvView]]><xsl:value-of select="TableBK12"/><![CDATA[" xml:space="preserve"> <value>View </value></data>
    <data name="dvViewLink]]><xsl:value-of select="TableBK12"/><![CDATA[" xml:space="preserve"> <value>]]><xsl:value-of select="TableBK12"/><![CDATA[</value></data>]]>
</xsl:if> 
<xsl:if test="TableBK13 !='NULL'">
    <![CDATA[<data name="dvView]]><xsl:value-of select="TableBK13"/><![CDATA[" xml:space="preserve"> <value>View </value></data>
    <data name="dvViewLink]]><xsl:value-of select="TableBK13"/><![CDATA[" xml:space="preserve"> <value>]]><xsl:value-of select="TableBK13"/><![CDATA[</value></data>]]>
</xsl:if> 
<xsl:if test="TableBK14 !='NULL'">
    <![CDATA[<data name="dvView]]><xsl:value-of select="TableBK14"/><![CDATA[" xml:space="preserve"> <value>View </value></data>
    <data name="dvViewLink]]><xsl:value-of select="TableBK14"/><![CDATA[" xml:space="preserve"> <value>]]><xsl:value-of select="TableBK14"/><![CDATA[</value></data>]]>
</xsl:if> 
<xsl:if test="TableBK15 !='NULL'">
    <![CDATA[<data name="dvView]]><xsl:value-of select="TableBK15"/><![CDATA[" xml:space="preserve"> <value>View </value></data>
    <data name="dvViewLink]]><xsl:value-of select="TableBK15"/><![CDATA[" xml:space="preserve"> <value>]]><xsl:value-of select="TableBK15"/><![CDATA[</value></data>]]>
</xsl:if> 
<xsl:if test="TableBK16 !='NULL'">
    <![CDATA[<data name="dvView]]><xsl:value-of select="TableBK16"/><![CDATA[" xml:space="preserve"> <value>View </value></data>
    <data name="dvViewLink]]><xsl:value-of select="TableBK16"/><![CDATA[" xml:space="preserve"> <value>]]><xsl:value-of select="TableBK16"/><![CDATA[</value></data>]]>
</xsl:if> 
<xsl:if test="TableBK17 !='NULL'">
    <![CDATA[<data name="dvView]]><xsl:value-of select="TableBK17"/><![CDATA[" xml:space="preserve"> <value>View </value></data>
    <data name="dvViewLink]]><xsl:value-of select="TableBK17"/><![CDATA[" xml:space="preserve"> <value>]]><xsl:value-of select="TableBK17"/><![CDATA[</value></data>]]>
</xsl:if> 
<xsl:if test="TableBK18 !='NULL'">
    <![CDATA[<data name="dvView]]><xsl:value-of select="TableBK18"/><![CDATA[" xml:space="preserve"> <value>View </value></data>
    <data name="dvViewLink]]><xsl:value-of select="TableBK18"/><![CDATA[" xml:space="preserve"> <value>]]><xsl:value-of select="TableBK18"/><![CDATA[</value></data>]]>
</xsl:if> 
<xsl:if test="TableBK19 !='NULL'">
    <![CDATA[<data name="dvView]]><xsl:value-of select="TableBK19"/><![CDATA[" xml:space="preserve"> <value>View </value></data>
    <data name="dvViewLink]]><xsl:value-of select="TableBK19"/><![CDATA[" xml:space="preserve"> <value>]]><xsl:value-of select="TableBK19"/><![CDATA[</value></data>]]>
</xsl:if> 
<xsl:if test="TableBK20 !='NULL'">
    <![CDATA[<data name="dvView]]><xsl:value-of select="TableBK20"/><![CDATA[" xml:space="preserve"> <value>View </value></data>
    <data name="dvViewLink]]><xsl:value-of select="TableBK20"/><![CDATA[" xml:space="preserve"> <value>]]><xsl:value-of select="TableBK20"/><![CDATA[</value></data>]]>
</xsl:if> 
<xsl:if test="TableBK20 !='NULL'">
    <![CDATA[<data name="dvView]]><xsl:value-of select="TableBK20"/><![CDATA[" xml:space="preserve"> <value>View </value></data>
    <data name="dvViewLink]]><xsl:value-of select="TableBK20"/><![CDATA[" xml:space="preserve"> <value>]]><xsl:value-of select="TableBK20"/><![CDATA[</value></data>]]>
</xsl:if> 
<!-- Ende View +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++  -->

<!-- Anfang Details +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ -->
<xsl:if test="TableFK1 !='NULL'">
    <![CDATA[<data name="dvDetail]]><xsl:value-of select="TableFK1"/><![CDATA[" xml:space="preserve"> <value>Detail ]]><xsl:value-of select="TableFK1"/><![CDATA[</value></data>]]>
</xsl:if>   
<xsl:if test="TableFK2 !='NULL'">
    <![CDATA[<data name="dvDetail]]><xsl:value-of select="TableFK2"/><![CDATA[" xml:space="preserve"> <value>Detail ]]><xsl:value-of select="TableFK2"/><![CDATA[</value></data>]]>
</xsl:if>   
<xsl:if test="TableBK1 !='NULL'">
    <![CDATA[<data name="dvDetail]]><xsl:value-of select="TableBK1"/><![CDATA[" xml:space="preserve"> <value>Detail ]]><xsl:value-of select="TableBK1"/><![CDATA[</value></data>]]>
</xsl:if>   
<xsl:if test="TableBK2 !='NULL'">
    <![CDATA[<data name="dvDetail]]><xsl:value-of select="TableBK2"/><![CDATA[" xml:space="preserve"> <value>Detail ]]><xsl:value-of select="TableBK2"/><![CDATA[</value></data>]]>
</xsl:if>   
<xsl:if test="TableBK3 !='NULL'">
    <![CDATA[<data name="dvDetail]]><xsl:value-of select="TableBK3"/><![CDATA[" xml:space="preserve"> <value>Detail ]]><xsl:value-of select="TableBK3"/><![CDATA[</value></data>]]>
</xsl:if>   
<xsl:if test="TableBK4 !='NULL'">
    <![CDATA[<data name="dvDetail]]><xsl:value-of select="TableBK4"/><![CDATA[" xml:space="preserve"> <value>Detail ]]><xsl:value-of select="TableBK4"/><![CDATA[</value></data>]]>
</xsl:if>   
<xsl:if test="TableBK5 !='NULL'">
    <![CDATA[<data name="dvDetail]]><xsl:value-of select="TableBK5"/><![CDATA[" xml:space="preserve"> <value>Detail ]]><xsl:value-of select="TableBK5"/><![CDATA[</value></data>]]>
</xsl:if>   
<xsl:if test="TableBK6 !='NULL'">
    <![CDATA[<data name="dvDetail]]><xsl:value-of select="TableBK6"/><![CDATA[" xml:space="preserve"> <value>Detail ]]><xsl:value-of select="TableBK6"/><![CDATA[</value></data>]]>
</xsl:if>   
<xsl:if test="TableBK7 !='NULL'">
    <![CDATA[<data name="dvDetail]]><xsl:value-of select="TableBK7"/><![CDATA[" xml:space="preserve"> <value>Detail ]]><xsl:value-of select="TableBK7"/><![CDATA[</value></data>]]>
</xsl:if>   
<xsl:if test="TableBK8 !='NULL'">
    <![CDATA[<data name="dvDetail]]><xsl:value-of select="TableBK8"/><![CDATA[" xml:space="preserve"> <value>Detail ]]><xsl:value-of select="TableBK8"/><![CDATA[</value></data>]]>
</xsl:if>   
<xsl:if test="TableBK9 !='NULL'">
    <![CDATA[<data name="dvDetail]]><xsl:value-of select="TableBK9"/><![CDATA[" xml:space="preserve"> <value>Detail ]]><xsl:value-of select="TableBK9"/><![CDATA[</value></data>]]>
</xsl:if>   
<xsl:if test="TableBK10 !='NULL'">
    <![CDATA[<data name="dvDetail]]><xsl:value-of select="TableBK10"/><![CDATA[" xml:space="preserve"> <value>Detail ]]><xsl:value-of select="TableBK10"/><![CDATA[</value></data>]]>
</xsl:if>   
<xsl:if test="TableBK11 !='NULL'">
    <![CDATA[<data name="dvDetail]]><xsl:value-of select="TableBK11"/><![CDATA[" xml:space="preserve"> <value>Detail ]]><xsl:value-of select="TableBK11"/><![CDATA[</value></data>]]>
</xsl:if>   
<xsl:if test="TableBK12 !='NULL'">
    <![CDATA[<data name="dvDetail]]><xsl:value-of select="TableBK12"/><![CDATA[" xml:space="preserve"> <value>Detail ]]><xsl:value-of select="TableBK12"/><![CDATA[</value></data>]]>
</xsl:if>   
<xsl:if test="TableBK13 !='NULL'">
    <![CDATA[<data name="dvDetail]]><xsl:value-of select="TableBK13"/><![CDATA[" xml:space="preserve"> <value>Detail ]]><xsl:value-of select="TableBK13"/><![CDATA[</value></data>]]>
</xsl:if>   
<xsl:if test="TableBK14 !='NULL'">
    <![CDATA[<data name="dvDetail]]><xsl:value-of select="TableBK14"/><![CDATA[" xml:space="preserve"> <value>Detail ]]><xsl:value-of select="TableBK14"/><![CDATA[</value></data>]]>
</xsl:if>   
<xsl:if test="TableBK15 !='NULL'">
    <![CDATA[<data name="dvDetail]]><xsl:value-of select="TableBK15"/><![CDATA[" xml:space="preserve"> <value>Detail ]]><xsl:value-of select="TableBK15"/><![CDATA[</value></data>]]>
</xsl:if>   
<xsl:if test="TableBK16 !='NULL'">
    <![CDATA[<data name="dvDetail]]><xsl:value-of select="TableBK16"/><![CDATA[" xml:space="preserve"> <value>Detail ]]><xsl:value-of select="TableBK16"/><![CDATA[</value></data>]]>
</xsl:if>   
<xsl:if test="TableBK17 !='NULL'">
    <![CDATA[<data name="dvDetail]]><xsl:value-of select="TableBK17"/><![CDATA[" xml:space="preserve"> <value>Detail ]]><xsl:value-of select="TableBK17"/><![CDATA[</value></data>]]>
</xsl:if>   
<xsl:if test="TableBK18 !='NULL'">
    <![CDATA[<data name="dvDetail]]><xsl:value-of select="TableBK18"/><![CDATA[" xml:space="preserve"> <value>Detail ]]><xsl:value-of select="TableBK18"/><![CDATA[</value></data>]]>
</xsl:if>   
<xsl:if test="TableBK19 !='NULL'">
    <![CDATA[<data name="dvDetail]]><xsl:value-of select="TableBK19"/><![CDATA[" xml:space="preserve"> <value>Detail ]]><xsl:value-of select="TableBK19"/><![CDATA[</value></data>]]>
</xsl:if>   
<xsl:if test="TableBK20 !='NULL'">
    <![CDATA[<data name="dvDetail]]><xsl:value-of select="TableBK20"/><![CDATA[" xml:space="preserve"> <value>Detail ]]><xsl:value-of select="TableBK20"/><![CDATA[</value></data>]]>
</xsl:if>   
<xsl:if test="TableBK21 !='NULL'">
    <![CDATA[<data name="dvDetail]]><xsl:value-of select="TableBK21"/><![CDATA[" xml:space="preserve"> <value>Detail ]]><xsl:value-of select="TableBK21"/><![CDATA[</value></data>]]>
</xsl:if>   
<!-- Ende Details ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++  -->  

<xsl:if test="Table !='NULL'">
    <![CDATA[<data name="header" xml:space="preserve"><value>Details of Table ]]><xsl:value-of select="Table"/><![CDATA[</value></data>]]>
</xsl:if>           

<![CDATA[

</root>
]]>

</xsl:template>
</xsl:stylesheet>