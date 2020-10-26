<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition"><![CDATA[<?xml version="1.0" encoding="utf-8"?>
<root>
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
		<value>System.Resources.ResXResourceReader, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>
	</resheader>
	<resheader name="writer">
		<value>System.Resources.ResXResourceWriter, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>
	</resheader>]]>
<!-- Anfang Schleife für alle +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ -->
<xsl:for-each select="//Fields/Field">
</xsl:for-each>
<!-- Ende Schleife  für alle ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ -->
          
<!-- Anfang Sonstiges +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ -->    
    <![CDATA[
    <data name="Create" xml:space="preserve"><value>Create ]]><xsl:value-of select="Basis"/><![CDATA[ </value></data>  
    <data name="Delete" xml:space="preserve"><value>Delete ]]><xsl:value-of select="Basis"/><![CDATA[  </value></data>  
    <data name="Details" xml:space="preserve"><value>Details ]]><xsl:value-of select="Basis"/><![CDATA[ : </value></data>  
    <data name="Edit" xml:space="preserve"><value>Edit ]]><xsl:value-of select="Basis"/><![CDATA[ </value></data>  
    <data name="Index" xml:space="preserve"><value>List of ]]><xsl:value-of select="Table"/><![CDATA[ </value></data>  
    <data name="]]><xsl:value-of select="ID"/><![CDATA[" xml:space="preserve"><value>]]><xsl:value-of select="ID"/><![CDATA[ </value></data>  ]]>
<xsl:choose>
<xsl:when test="Table ='Name+++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">      
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">      
</xsl:when>
<xsl:when test="Table ='Tbl90References'">      
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">      
</xsl:when>
<xsl:when test="Table ='TblCounters'">      
</xsl:when>
<xsl:otherwise>  
<![CDATA[  
    <data name="]]><xsl:value-of select="Name"/><![CDATA[" xml:space="preserve"><value>]]><xsl:value-of select="Name"/><![CDATA[</value></data>  ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:if test="Table='AspnetApplications'">      <![CDATA[
    <data name="LoweredApplicationName" xml:space="preserve"><value>LoweredApplicationName</value></data>  
    <data name="Description" xml:space="preserve"><value>Description</value></data>  ]]>
</xsl:if>  
<xsl:if test="Table='AspnetMemberships'">      <![CDATA[
    <data name="Password" xml:space="preserve"><value>Password</value></data>  
    <data name="PasswordFormat" xml:space="preserve"><value>PasswordFormat</value></data>  
    <data name="PasswordSalt" xml:space="preserve"><value>PasswordSalt</value></data>  
    <data name="MobilePIN" xml:space="preserve"><value>MobilePIN</value></data>  
    <data name="Email" xml:space="preserve"><value>Email</value></data>  
    <data name="LoweredEmail" xml:space="preserve"><value>LoweredEmail</value></data>  
    <data name="PasswordQuestion" xml:space="preserve"><value>PasswordQuestion</value></data>  
    <data name="PasswordAnswer" xml:space="preserve"><value>PasswordAnswer</value></data>  
    <data name="IsApproved" xml:space="preserve"><value>IsApproved</value></data>  
    <data name="IsLockedOut" xml:space="preserve"><value>IsLockedOut</value></data>  
    <data name="CreateDate" xml:space="preserve"><value>CreateDate</value></data>  
    <data name="LastLoginDate" xml:space="preserve"><value>LastLoginDate</value></data>  
    <data name="LastPasswordChangedDate" xml:space="preserve"><value>LastPasswordChangedDate</value></data>  
    <data name="LastLockoutDate" xml:space="preserve"><value>LastLockoutDate</value></data>  
    <data name="FailedPasswordAttemptCount" xml:space="preserve"><value>FailedPasswordAttemptCount</value></data>  
    <data name="FailedPasswordAttemptWindowStart" xml:space="preserve"><value>FailedPasswordAttemptWindowStart</value></data>  
    <data name="FailedPasswordAnswerAttemptCount" xml:space="preserve"><value>FailedPasswordAnswerAttemptCount</value></data>  
    <data name="FailedPasswordAnswerAttemptWindowStart" xml:space="preserve"><value>FailedPasswordAnswerAttemptWindowStart</value></data>  
    <data name="Comment" xml:space="preserve"><value>Comment</value></data>  ]]>
</xsl:if>  
<xsl:if test="Table='AspnetUsers'">      <![CDATA[
    <data name="LoweredUserName" xml:space="preserve"><value>LoweredUserName</value></data>  
    <data name="MobileAlias" xml:space="preserve"><value>MobileAlias</value></data>  
    <data name="IsAnonymous" xml:space="preserve"><value>IsAnonymous</value></data>  
    <data name="LastActivityDate" xml:space="preserve"><value>LastActivityDate</value></data>  ]]>
</xsl:if>  
<xsl:if test="Table='Tbl03Regnums'">      <![CDATA[
    <data name="Subregnum" xml:space="preserve"><value>Subregnum</value></data>  ]]>
</xsl:if>  
<xsl:if test="Table='Tbl68Speciesgroups'">      <![CDATA[
    <data name="Subspeciesgroup" xml:space="preserve"><value>Subspeciesgroup</value></data>  ]]>
</xsl:if>    
<xsl:if test="Table='Tbl69FiSpeciesses'">      <![CDATA[
    <data name="Subspecies" xml:space="preserve"><value>Subspecies</value></data>  
    <data name="Divers" xml:space="preserve"><value>Divers</value></data>  
    <data name="BasinHeight" xml:space="preserve"><value>BasinHeight</value></data>  
    <data name="CarboHardness1" xml:space="preserve"><value>CarboHardness1</value></data>  
    <data name="CarboHardness2" xml:space="preserve"><value>-</value></data>  
    <data name="Difficult1" xml:space="preserve"><value>Difficult1</value></data>  
    <data name="Difficult2" xml:space="preserve"><value>Difficult2</value></data>  
    <data name="Difficult3" xml:space="preserve"><value>Difficult3</value></data>  
    <data name="Difficult4" xml:space="preserve"><value>Difficult4</value></data>  
    <data name="Difficult1_4" xml:space="preserve"><value>Difficult1_4</value></data>  
    <data name="FishLength" xml:space="preserve"><value>FishLength</value></data>  
    <data name="Hardness1" xml:space="preserve"><value>Hardness1</value></data>  
    <data name="Hardness2" xml:space="preserve"><value>-</value></data>  
    <data name="Herbivore" xml:space="preserve"><value>Herbivore</value></data>  
    <data name="Importer" xml:space="preserve"><value>Importer</value></data>  
    <data name="Importer_Year" xml:space="preserve"><value>Importer_Year</value></data>  
    <data name="ImportingYear" xml:space="preserve"><value>ImportingYear</value></data>  
    <data name="Karnivore" xml:space="preserve"><value>Karnivore</value></data>  
    <data name="LDANumber" xml:space="preserve"><value>LDANumber</value></data>  
    <data name="LDANumber_LDAOrigin" xml:space="preserve"><value>LDANumber_LDAOrigin</value></data>  
    <data name="LDAOrigin" xml:space="preserve"><value>LDAOrigin</value></data>  
    <data name="Limnivore" xml:space="preserve"><value>Limnivore</value></data>  
    <data name="LNumber" xml:space="preserve"><value>LNumber</value></data>  
    <data name="LNumber_LOrigin" xml:space="preserve"><value>LNumber_LOrigin</value></data>  
    <data name="LOrigin" xml:space="preserve"><value>LOrigin</value></data>  
    <data name="MemoBreeding" xml:space="preserve"><value>MemoBreeding</value></data>  
    <data name="MemoBuilt" xml:space="preserve"><value>MemoBuilt</value></data>  
    <data name="MemoColor" xml:space="preserve"><value>MemoColor</value></data>  
    <data name="MemoDomorphism" xml:space="preserve"><value>MemoDomorphism</value></data>  
    <data name="MemoFoods" xml:space="preserve"><value>MemoFoods</value></data>  
    <data name="MemoHusbandry" xml:space="preserve"><value>MemoHusbandry</value></data>  
    <data name="MemoRegion" xml:space="preserve"><value>MemoRegion</value></data>  
    <data name="MemoSozial" xml:space="preserve"><value>MemoSozial</value></data>  
    <data name="MemoSpecial" xml:space="preserve"><value>MemoSpecial</value></data>  
    <data name="MemoSpecies" xml:space="preserve"><value>MemoSpecies</value></data>  
    <data name="MemoTech" xml:space="preserve"><value>MemoTech</value></data>  
    <data name="Omnivore" xml:space="preserve"><value>Omnivore</value></data>  
    <data name="Ph1" xml:space="preserve"><value>Ph1</value></data>  
    <data name="Ph2" xml:space="preserve"><value>-</value></data>  
    <data name="RegionBottom" xml:space="preserve"><value>RegionBottom</value></data>  
    <data name="RegionMiddle" xml:space="preserve"><value>RegionMiddle</value></data>  
    <data name="RegionTop" xml:space="preserve"><value>RegionTop</value></data>  
    <data name="Temp1" xml:space="preserve"><value>Temp1</value></data>  
    <data name="Temp2" xml:space="preserve"><value>-</value></data>  
    <data name="TradeName" xml:space="preserve"><value>TradeName</value></data>  
    <data name="TypeSpecies" xml:space="preserve"><value>TypeSpecies</value></data>  ]]>
</xsl:if>   
<xsl:if test="Table='Tbl72PlSpeciesses'">      <![CDATA[
    <data name="Subspecies" xml:space="preserve"><value>Subspecies</value></data>  
    <data name="Divers" xml:space="preserve"><value>Divers</value></data>  
    <data name="BasinHeight" xml:space="preserve"><value>BasinHeight</value></data>  
    <data name="CarboHardness1" xml:space="preserve"><value>CarboHardness1</value></data>  
    <data name="CarboHardness2" xml:space="preserve"><value>-</value></data>  
    <data name="Difficult1" xml:space="preserve"><value>Difficult1</value></data>  
    <data name="Difficult2" xml:space="preserve"><value>Difficult2</value></data>  
    <data name="Difficult3" xml:space="preserve"><value>Difficult3</value></data>  
    <data name="Difficult4" xml:space="preserve"><value>Difficult4</value></data>  
    <data name="Difficult1_4" xml:space="preserve"><value>Difficult1_4</value></data>  
    <data name="Hardness1" xml:space="preserve"><value>Hardness1</value></data>  
    <data name="Hardness2" xml:space="preserve"><value>-</value></data>  
    <data name="Importer" xml:space="preserve"><value>Importer</value></data>  
    <data name="Importer_Year" xml:space="preserve"><value>Importer_Year</value></data>  
    <data name="ImportingYear" xml:space="preserve"><value>ImportingYear</value></data>  
    <data name="MemoBuilt" xml:space="preserve"><value>MemoBuilt</value></data>  
    <data name="MemoColor" xml:space="preserve"><value>MemoColor</value></data>  
    <data name="MemoCulture" xml:space="preserve"><value>MemoCulture</value></data>  
    <data name="MemoGlobal" xml:space="preserve"><value>MemoGlobal</value></data>  
    <data name="MemoReproduction" xml:space="preserve"><value>MemoReproduction</value></data>  
    <data name="MemoSpecies" xml:space="preserve"><value>MemoSpecies</value></data>  
    <data name="MemoTech" xml:space="preserve"><value>MemoTech</value></data>  
    <data name="Ph1" xml:space="preserve"><value>Ph1</value></data>  
    <data name="Ph2" xml:space="preserve"><value>-</value></data>  
    <data name="PlantLength" xml:space="preserve"><value>PlantLength</value></data>  
    <data name="Temp1" xml:space="preserve"><value>Temp1</value></data>  
    <data name="Temp2" xml:space="preserve"><value>-</value></data>  
    <data name="TradeName" xml:space="preserve"><value>TradeName</value></data>  ]]>
</xsl:if>   
<xsl:if test="Table='Tbl78Names'">      <![CDATA[
    <data name="TitleGenus" xml:space="preserve"><value>Genus </value></data>  
    <data name="TitleSpecies" xml:space="preserve"><value>Species </value></data>  
    <data name="Language" xml:space="preserve"><value>Language </value></data>  ]]>
</xsl:if>   
<xsl:if test="Table='Tbl81Images'">      <![CDATA[
    <data name="TitleGenus" xml:space="preserve"><value>Genus </value></data>  
    <data name="TitleSpecies" xml:space="preserve"><value>Species </value></data>  
    <data name="UploadImage" xml:space="preserve"><value>UploadImage </value></data>  
    <data name="ShotDate" xml:space="preserve"><value>ShotDate </value></data>  
    <data name="FilestreamID" xml:space="preserve"><value>FilestreamID </value></data>  
    <data name="Filestream" xml:space="preserve"><value>Filestream </value></data>  
    <data name="ImageMimeType" xml:space="preserve"><value>ImageMimeType </value></data>  ]]>
</xsl:if>   
<xsl:if test="Table='Tbl84Synonyms'">      <![CDATA[
    <data name="TitleGenus" xml:space="preserve"><value>Genus </value></data>  
    <data name="TitleSpecies" xml:space="preserve"><value>Species </value></data>  ]]>
</xsl:if>   
<xsl:if test="Table='Tbl87Geographics'">      <![CDATA[
    <data name="TitleGenus" xml:space="preserve"><value>Genus </value></data>  
    <data name="TitleSpecies" xml:space="preserve"><value>Species </value></data>  
    <data name="Country" xml:space="preserve"><value>Country </value></data>  
    <data name="Address" xml:space="preserve"><value>Address </value></data>  
    <data name="Latitude" xml:space="preserve"><value>Latitude </value></data>  
    <data name="Longitude" xml:space="preserve"><value>Longitude </value></data>  ]]>
</xsl:if>   
<xsl:if test="Table='Tbl90RefAuthors'">      <![CDATA[
    <data name="ArticelTitle" xml:space="preserve"><value>ArticelTitle </value></data>  
    <data name="BookName" xml:space="preserve"><value>BookName </value></data>  
    <data name="ISBN" xml:space="preserve"><value>ISBN </value></data>  
    <data name="Notes" xml:space="preserve"><value>Notes </value></data>  
    <data name="Page" xml:space="preserve"><value>Page </value></data>  
    <data name="PublicationPlace" xml:space="preserve"><value>PublicationPlace </value></data>  
    <data name="PublicationYear" xml:space="preserve"><value>PublicationYear </value></data>  
    <data name="Publisher" xml:space="preserve"><value>Publisher </value></data>  ]]>
</xsl:if>   
<xsl:if test="Table='Tbl90References'">      <![CDATA[
    <data name="RefAuthor" xml:space="preserve"><value>RefAuthor </value></data>  
    <data name="RefSource" xml:space="preserve"><value>RefSource </value></data>  
    <data name="RefExpert" xml:space="preserve"><value>RefExpert </value></data>  
    <data name="Page" xml:space="preserve"><value>Page </value></data>  
    <data name="PublicationYear" xml:space="preserve"><value>PublicationYear </value></data>  
    <data name="SourceYear" xml:space="preserve"><value>SourceYear </value></data>  
    <data name="Taxo" xml:space="preserve"><value>Taxo </value></data>  

    <data name="DDLClassName" xml:space="preserve"><value>Class </value></data>  
    <data name="DDLDivisionName" xml:space="preserve"><value>Division </value></data>  
    <data name="DDLFamilyName" xml:space="preserve"><value>Family </value></data>  
    <data name="DDLFiSpeciesName" xml:space="preserve"><value>FiSpecies </value></data>  
    <data name="DDLGenusName" xml:space="preserve"><value>Genus </value></data>  
    <data name="DDLInfraclassName" xml:space="preserve"><value>Infraclass </value></data>  
    <data name="DDLInfrafamilyName" xml:space="preserve"><value>Infrafamily </value></data>  
    <data name="DDLInfraordoName" xml:space="preserve"><value>Infraordo </value></data>  
    <data name="DDLInfratribusName" xml:space="preserve"><value>Infratribus </value></data>  
    <data name="DDLLegioName" xml:space="preserve"><value>Legio </value></data>  
    <data name="DDLOrdoName" xml:space="preserve"><value>Ordo </value></data>  
    <data name="DDLPhylumName" xml:space="preserve"><value>Phylum </value></data>  
    <data name="DDLPlSpeciesName" xml:space="preserve"><value>PlSpecies </value></data>  
    <data name="DDLRegnumName" xml:space="preserve"><value>Regnum </value></data>  
    <data name="DDLSubclassName" xml:space="preserve"><valueSubclass </value></data>  
    <data name="DDLSubdivisionName" xml:space="preserve"><value>Subdivision </value></data>  
    <data name="DDLSubfamilyName" xml:space="preserve"><value>Subfamily </value></data>  
    <data name="DDLSubordoName" xml:space="preserve"><value>Subordo </value></data>  
    <data name="DDLSubphylumName" xml:space="preserve"><value>Subphylum </value></data>  
    <data name="DDLSubtribusName" xml:space="preserve"><value>Subtribus </value></data>  
    <data name="DDLSuperclassName" xml:space="preserve"><value>Superclass </value></data>  
    <data name="DDLSuperfamilyName" xml:space="preserve"><value>Superfamily </value></data>  
    <data name="DDLSupertribusName" xml:space="preserve"><value>Supertribus </value></data>  
    <data name="DDLTribusName" xml:space="preserve"><value>Tribus </value></data>  ]]>
</xsl:if>   
<xsl:if test="Table='Tbl90RefExperts'">      <![CDATA[
    <data name="Notes" xml:space="preserve"><value>Notes </value></data>  ]]>
</xsl:if>   
<xsl:if test="Table='Tbl90RefSources'">      <![CDATA[
    <data name="SourceYear" xml:space="preserve"><value>SourceYear </value></data>  
    <data name="Notes" xml:space="preserve"><value>Notes </value></data>  ]]>
</xsl:if>   
<xsl:if test="Table='Tbl93Comments'">      <![CDATA[
    <data name="Taxo" xml:space="preserve"><value>Taxo </value></data>  

    <data name="DDLClassName" xml:space="preserve"><value>Class </value></data>  
    <data name="DDLDivisionName" xml:space="preserve"><value>Division </value></data>  
    <data name="DDLFamilyName" xml:space="preserve"><value>Family </value></data>  
    <data name="DDLFiSpeciesName" xml:space="preserve"><value>FiSpecies </value></data>  
    <data name="DDLGenusName" xml:space="preserve"><value>Genus </value></data>  
    <data name="DDLInfraclassName" xml:space="preserve"><value>Infraclass </value></data>  
    <data name="DDLInfrafamilyName" xml:space="preserve"><value>Infrafamily </value></data>  
    <data name="DDLInfraordoName" xml:space="preserve"><value>Infraordo </value></data>  
    <data name="DDLInfratribusName" xml:space="preserve"><value>Infratribus </value></data>  
    <data name="DDLLegioName" xml:space="preserve"><value>Legio </value></data>  
    <data name="DDLOrdoName" xml:space="preserve"><value>Ordo </value></data>  
    <data name="DDLPhylumName" xml:space="preserve"><value>Phylum </value></data>  
    <data name="DDLPlSpeciesName" xml:space="preserve"><value>PlSpecies </value></data>  
    <data name="DDLRegnumName" xml:space="preserve"><value>Regnum </value></data>  
    <data name="DDLSubclassName" xml:space="preserve"><valueSubclass </value></data>  
    <data name="DDLSubdivisionName" xml:space="preserve"><value>Subdivision </value></data>  
    <data name="DDLSubfamilyName" xml:space="preserve"><value>Subfamily </value></data>  
    <data name="DDLSubordoName" xml:space="preserve"><value>Subordo </value></data>  
    <data name="DDLSubphylumName" xml:space="preserve"><value>Subphylum </value></data>  
    <data name="DDLSubtribusName" xml:space="preserve"><value>Subtribus </value></data>  
    <data name="DDLSuperclassName" xml:space="preserve"><value>Superclass </value></data>  
    <data name="DDLSuperfamilyName" xml:space="preserve"><value>Superfamily </value></data>  
    <data name="DDLSupertribusName" xml:space="preserve"><value>Supertribus </value></data>  
    <data name="DDLTribusName" xml:space="preserve"><value>Tribus </value></data>  ]]>
</xsl:if>   




<!-- Ende Sonstiges +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ -->
          
<![CDATA[

</root>
]]>

</xsl:template>
</xsl:stylesheet>




