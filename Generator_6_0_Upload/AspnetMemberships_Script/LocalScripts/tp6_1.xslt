<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition">

<xsl:choose>
<xsl:when test="Table ='Usercontrol Header  Top+++++++++++++'">        
</xsl:when>
<xsl:otherwise>          <![CDATA[  
<UserControl x:Class="Atis.WpfUi.View.]]><xsl:value-of select="Table"/><![CDATA[View"               
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
             xmlns:i="http://schemas.microsoft.com/expression/2010/interactivity"          
             xmlns:command="http://www.galasoft.ch/mvvmlight"
   ]]>                                            
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='Usercontrol Header  Middle+++++++++++++'">        
</xsl:when>
<xsl:otherwise>
  <xsl:if test="TableBK1 !='NULL'">
       <![CDATA[      xmlns:]]><xsl:value-of select="TranslateBK1"/><![CDATA[Res="clr-namespace:]]><xsl:value-of select="TableBK1"/><![CDATA[Res;assembly=Atis.Resources"                   ]]> 
  </xsl:if> 
  <xsl:if test="TableBK2 !='NULL'">
       <![CDATA[      xmlns:]]><xsl:value-of select="TranslateBK2"/><![CDATA[Res="clr-namespace:]]><xsl:value-of select="TableBK2"/><![CDATA[Res;assembly=Atis.Resources"                   ]]> 
  </xsl:if> 
  <xsl:if test="TableFK1 !='NULL'">
       <![CDATA[      xmlns:]]><xsl:value-of select="TranslateFK1"/><![CDATA[Res="clr-namespace:]]><xsl:value-of select="TableFK1"/><![CDATA[Res;assembly=Atis.Resources"                   ]]> 
  </xsl:if> 
  <xsl:if test="TableFK2 !='NULL'">
       <![CDATA[      xmlns:]]><xsl:value-of select="TranslateFK2"/><![CDATA[Res="clr-namespace:]]><xsl:value-of select="TableFK2"/><![CDATA[Res;assembly=Atis.Resources"                   ]]> 
  </xsl:if> 
  <xsl:if test="Table !='NULL'">
       <![CDATA[      xmlns:]]><xsl:value-of select="Translate"/><![CDATA[Res="clr-namespace:]]><xsl:value-of select="Table"/><![CDATA[Res;assembly=Atis.Resources"                   ]]> 
  </xsl:if> 
  <xsl:if test="TableTK1 !='NULL'">
       <![CDATA[      xmlns:]]><xsl:value-of select="TranslateTK1"/><![CDATA[Res="clr-namespace:]]><xsl:value-of select="TableTK1"/><![CDATA[Res;assembly=Atis.Resources"                   ]]> 
  </xsl:if> 
  <xsl:if test="TableTK2 !='NULL'">
       <![CDATA[      xmlns:]]><xsl:value-of select="TranslateTK2"/><![CDATA[Res="clr-namespace:]]><xsl:value-of select="TableTK2"/><![CDATA[Res;assembly=Atis.Resources"                   ]]> 
  </xsl:if>         <![CDATA[  
             xmlns:tbl90RefExpertsRes="clr-namespace:Tbl90RefExpertsRes;assembly=Atis.Resources"
             xmlns:tbl90RefAuthorsRes="clr-namespace:Tbl90RefAuthorsRes;assembly=Atis.Resources"
             xmlns:tbl90RefSourcesRes="clr-namespace:Tbl90RefSourcesRes;assembly=Atis.Resources"
             xmlns:tbl90ReferencesRes="clr-namespace:Tbl90ReferencesRes;assembly=Atis.Resources"
             xmlns:tbl93CommentsRes="clr-namespace:Tbl93CommentsRes;assembly=Atis.Resources"
             xmlns:sharedRes="clr-namespace:SharedRes;assembly=Atis.Resources"
             mc:Ignorable="d" 
             d:DesignHeight="800" d:DesignWidth="1250">
   ]]>                                            
</xsl:otherwise>    
</xsl:choose>

<![CDATA[        <!-- ]]><xsl:value-of select="Table"/><![CDATA[View.xaml  Skriptdatum: ]]> <xsl:value-of select="DateTime"/><![CDATA[   -->    ]]>

<xsl:choose>
<xsl:when test="Table ='Grid Start+++++++++++++'">        
</xsl:when>
<xsl:otherwise>  
         <![CDATA[  
    <Grid>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="*" />
            <ColumnDefinition Width="Auto" />
        </Grid.ColumnDefinitions>
   ]]>                                            
</xsl:otherwise>    
</xsl:choose>
                                                                                                                                                                
<xsl:choose>
<xsl:when test="Table ='HeaderedContentControl Left Basic ListView ++++++++++++++++'"> 
</xsl:when>
<xsl:otherwise>  
         <![CDATA[  
         <HeaderedContentControl Style="{StaticResource SubDisplayArea}" Grid.Column="0" Header="{x:Static sharedRes:StringsRes.]]><xsl:value-of select="Basis"/><![CDATA[}" Width="auto" HorizontalAlignment="Stretch" >
            <StackPanel >                                              
               <HeaderedContentControl>
                    <HeaderedContentControl.Header>
                        <StackPanel Orientation="Horizontal" HorizontalAlignment="Right" >
                            <TextBlock Margin="2"  Text="{x:Static ]]><xsl:value-of select="Translate"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="Name"/><![CDATA[}"   HorizontalAlignment="Left"/>
                            <TextBox Margin="10,2" Width="100" VerticalAlignment="Center" Text="{Binding Path=Search]]><xsl:value-of select="Name"/><![CDATA[, UpdateSourceTrigger=PropertyChanged, Mode=TwoWay}" />
                            <Button  Content="{x:Static sharedRes:StringsRes.Search}" DockPanel.Dock="Right" Margin="10,2" VerticalAlignment="Center"
                                Command="{Binding Path=Get]]><xsl:value-of select="Basis"/><![CDATA[ByNameCommand}" IsDefault="True" Width="120" />
                            <TextBlock Margin="0,0,5,0">
                                <Hyperlink Command="{Binding Path=Add]]><xsl:value-of select="Basis"/><![CDATA[Command}"  >
                                    <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkCreate}"/>
                                </Hyperlink>
                              | <Hyperlink Command="{Binding Path=Delete]]><xsl:value-of select="Basis"/><![CDATA[Command}">
                                    <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkDelete}"/>
                                </Hyperlink>
                            </TextBlock>
                        </StackPanel>
                    </HeaderedContentControl.Header>
                    <ListView x:Name="]]><xsl:value-of select="Table"/><![CDATA[List" SelectedItem="{Binding SelectedItem}" ItemsSource="{Binding ]]><xsl:value-of select="Table"/><![CDATA[List}" IsSynchronizedWithCurrentItem="True" Margin="0,0,0,0" Height="250"  >
                        <i:Interaction.Triggers>
                            <i:EventTrigger EventName="MouseDoubleClick">
                                <command:EventToCommand Command="{Binding GetConnectedTablesCommand}" 
                                     CommandParameter="{Binding ElementName=SelectedItem,Path=SelectedItem}"     />
                            </i:EventTrigger>
                        </i:Interaction.Triggers>
                        <ListView.View>
                            <GridView>
                                <GridViewColumn DisplayMemberBinding="{Binding ]]><xsl:value-of select="ID"/><![CDATA[}" Header="{x:Static ]]><xsl:value-of select="Translate"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="ID"/><![CDATA[}" Width="80" />
                                <GridViewColumn DisplayMemberBinding="{Binding ]]><xsl:value-of select="Name"/><![CDATA[}" Header="{x:Static ]]><xsl:value-of select="Translate"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="Name"/><![CDATA[}" Width="250" />     ]]> 
<xsl:choose>
<xsl:when test="Table ='++++++Subregnum++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">
       <![CDATA[                         <GridViewColumn DisplayMemberBinding="{Binding Subregnum}" Header="{x:Static ]]><xsl:value-of select="Translate"/><![CDATA[Res:StringsRes.Subregnum}" Width="250"/>                  ]]> 
</xsl:when>  
<xsl:otherwise>
</xsl:otherwise>    
</xsl:choose>      <![CDATA[                          <GridViewColumn >
                                    <GridViewColumn.Header >
                                        <Label Content="{x:Static sharedRes:StringsRes.Valid}"/>
                                    </GridViewColumn.Header>
                                    <GridViewColumn.CellTemplate >
                                        <DataTemplate>
                                            <StackPanel Orientation="Horizontal" >
                                                <CheckBox IsChecked="{Binding Path=Valid}"  />
                                            </StackPanel>
                                        </DataTemplate>
                                    </GridViewColumn.CellTemplate>
                                </GridViewColumn>
                            </GridView>
                        </ListView.View>
                    </ListView>
                </HeaderedContentControl>             ]]>                                            
</xsl:otherwise>    
</xsl:choose>
                                 
<xsl:choose>
<xsl:when test="Table ='++++++FK1++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">
</xsl:when>  
<xsl:otherwise>      <![CDATA[  
                <StackPanel>
                    <TabControl Padding="0" Margin="1,1,1,1" SelectedIndex="{Binding SelectedMainTabIndex,  UpdateSourceTrigger=PropertyChanged , Mode=TwoWay}" Height="auto">
                        <TabItem Header="{x:Static sharedRes:StringsRes.TabStripHeader]]><xsl:value-of select="BasissFK1"/><![CDATA[}" >
                         <HeaderedContentControl>
                                    <HeaderedContentControl.Header>
                                        <StackPanel Orientation="Horizontal" HorizontalAlignment="Right">
                              <TextBlock Margin="2"  Text="{x:Static ]]><xsl:value-of select="TranslateFK1"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[}"   HorizontalAlignment="Left"/>
                                          <TextBox Margin="10,2" Width="100" VerticalAlignment="Center" Text="{Binding Path=Search]]><xsl:value-of select="NameFK1"/><![CDATA[, UpdateSourceTrigger=PropertyChanged , Mode=TwoWay}" />
                                            <Button Content="{x:Static sharedRes:StringsRes.Search}" DockPanel.Dock="Right" Margin="10,2" VerticalAlignment="Center"
                                                    Command="{Binding Path=Get]]><xsl:value-of select="BasisFK1"/><![CDATA[ByNameCommand}" IsDefault="True" Width="120" />
                                            <TextBlock Margin="0,0,5,0">
                                                <Hyperlink Command="{Binding Path=Add]]><xsl:value-of select="BasisFK1"/><![CDATA[Command}"  >
                                                    <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkCreate}"/>
                                                </Hyperlink>
                                              | <Hyperlink Command="{Binding Path=Delete]]><xsl:value-of select="BasisFK1"/><![CDATA[Command}">
                                                    <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkDelete}"/>
                                                </Hyperlink>
                                            </TextBlock>
                                        </StackPanel>
                                    </HeaderedContentControl.Header>
                                    <ListView x:Name="]]><xsl:value-of select="TableFK1"/><![CDATA[List" ItemsSource="{Binding ]]><xsl:value-of select="TableFK1"/><![CDATA[List}" IsSynchronizedWithCurrentItem="True" Margin="0,0,0,0" Height="400" >
                                        <ListView.View>
                                            <GridView>
                                                <GridViewColumn DisplayMemberBinding="{Binding ]]><xsl:value-of select="IDFK1"/><![CDATA[}" Header="{x:Static ]]><xsl:value-of select="TranslateFK1"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="IDFK1"/><![CDATA[}" Width="80" />
                                                <GridViewColumn DisplayMemberBinding="{Binding ]]><xsl:value-of select="NameFK1"/><![CDATA[}" Header="{x:Static ]]><xsl:value-of select="TranslateFK1"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[}" Width="250" />     ]]> 
<xsl:choose>
<xsl:when test="Table ='++++++Subregnum++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl06Phylums'">
       <![CDATA[                         <GridViewColumn DisplayMemberBinding="{Binding Subregnum}" Header="{x:Static ]]><xsl:value-of select="TranslateFK1"/><![CDATA[Res:StringsRes.Subregnum}" Width="250"/>                  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl09Divisions'">
       <![CDATA[                         <GridViewColumn DisplayMemberBinding="{Binding Subregnum}" Header="{x:Static ]]><xsl:value-of select="TranslateFK1"/><![CDATA[Res:StringsRes.Subregnum}" Width="250"/>                  ]]> 
</xsl:when>  
<xsl:otherwise>
</xsl:otherwise>    
</xsl:choose>      <![CDATA[                          <GridViewColumn >

                                                    <GridViewColumn.Header >
                                                        <Label Content="{x:Static sharedRes:StringsRes.Valid}"/>
                                                    </GridViewColumn.Header>
                                                    <GridViewColumn.CellTemplate >
                                                        <DataTemplate>
                                                            <StackPanel Orientation="Horizontal" >
                                                                <CheckBox IsChecked="{Binding Path=Valid}"  />
                                                            </StackPanel>
                                                        </DataTemplate>
                                                    </GridViewColumn.CellTemplate>
                                                </GridViewColumn>
                                                <GridViewColumn DisplayMemberBinding="{Binding Author}" Header="{x:Static sharedRes:StringsRes.Author}" Width="150" />
                                                <GridViewColumn DisplayMemberBinding="{Binding AuthorYear}" Header="{x:Static sharedRes:StringsRes.AuthorYear}" Width="80" />
                                            </GridView>
                                        </ListView.View>
                                    </ListView>
                                </HeaderedContentControl>
                        </TabItem>      ]]> 
</xsl:otherwise>    
</xsl:choose>                              
  
<xsl:choose>
<xsl:when test="Table ='++++++FK2++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl18Superclasses'">
     <![CDATA[  
                        <TabItem Header="{x:Static sharedRes:StringsRes.TabStripHeader]]><xsl:value-of select="BasissFK2"/><![CDATA[}" >
                         <HeaderedContentControl>
                                    <HeaderedContentControl.Header>
                                        <StackPanel Orientation="Horizontal" HorizontalAlignment="Right">
                              <TextBlock Margin="2"  Text="{x:Static ]]><xsl:value-of select="TranslateFK2"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="NameFK2"/><![CDATA[}"   HorizontalAlignment="Left"/>
                                          <TextBox Margin="10,2" Width="100" VerticalAlignment="Center" Text="{Binding Path=Search]]><xsl:value-of select="NameFK2"/><![CDATA[, UpdateSourceTrigger=PropertyChanged , Mode=TwoWay}" />
                                            <Button Content="{x:Static sharedRes:StringsRes.Search}" DockPanel.Dock="Right" Margin="10,2" VerticalAlignment="Center"
                                                    Command="{Binding Path=Get]]><xsl:value-of select="BasisFK2"/><![CDATA[ByNameCommand}" IsDefault="True" Width="120" />
                                            <TextBlock Margin="0,0,5,0">
                                                <Hyperlink Command="{Binding Path=Add]]><xsl:value-of select="BasisFK2"/><![CDATA[Command}"  >
                                                    <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkCreate}"/>
                                                </Hyperlink>
                                              | <Hyperlink Command="{Binding Path=Delete]]><xsl:value-of select="BasisFK2"/><![CDATA[Command}">
                                                    <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkDelete}"/>
                                                </Hyperlink>
                                            </TextBlock>
                                        </StackPanel>
                                    </HeaderedContentControl.Header>
                                    <ListView x:Name="]]><xsl:value-of select="TableFK2"/><![CDATA[List" ItemsSource="{Binding ]]><xsl:value-of select="TableFK2"/><![CDATA[List}" IsSynchronizedWithCurrentItem="True" Margin="0,0,0,0" Height="400" >
                                        <ListView.View>
                                            <GridView>
                                                <GridViewColumn DisplayMemberBinding="{Binding ]]><xsl:value-of select="IDFK2"/><![CDATA[}" Header="{x:Static ]]><xsl:value-of select="TranslateFK2"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="IDFK2"/><![CDATA[}" Width="80" />
                                                <GridViewColumn DisplayMemberBinding="{Binding ]]><xsl:value-of select="NameFK2"/><![CDATA[}" Header="{x:Static ]]><xsl:value-of select="TranslateFK2"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="NameFK2"/><![CDATA[}" Width="250" />     
                                                <GridViewColumn >
                                                    <GridViewColumn.Header >
                                                        <Label Content="{x:Static sharedRes:StringsRes.Valid}"/>
                                                    </GridViewColumn.Header>
                                                    <GridViewColumn.CellTemplate >
                                                        <DataTemplate>
                                                            <StackPanel Orientation="Horizontal" >
                                                                <CheckBox IsChecked="{Binding Path=Valid}"  />
                                                            </StackPanel>
                                                        </DataTemplate>
                                                    </GridViewColumn.CellTemplate>
                                                </GridViewColumn>
                                                <GridViewColumn DisplayMemberBinding="{Binding Author}" Header="{x:Static sharedRes:StringsRes.Author}" Width="150" />
                                                <GridViewColumn DisplayMemberBinding="{Binding AuthorYear}" Header="{x:Static sharedRes:StringsRes.AuthorYear}" Width="80" />
                                            </GridView>
                                        </ListView.View>
                                    </ListView>
                                </HeaderedContentControl>
                        </TabItem>      ]]> 
</xsl:when>  
<xsl:otherwise> 
</xsl:otherwise>    
</xsl:choose>                              
  
<xsl:choose>
<xsl:when test="Table ='++++++Tab Control++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">   <![CDATA[ 
                 <StackPanel>
                    <TabControl Padding="0" Margin="1,1,1,1" SelectedIndex="{Binding SelectedMainTabIndex, UpdateSourceTrigger=PropertyChanged, Mode=TwoWay}" Height="auto"> ]]>   
</xsl:when>  
<xsl:otherwise>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++TK1++++++++'">
</xsl:when>  
<xsl:otherwise>      <![CDATA[  
                       <TabItem Header="{x:Static sharedRes:StringsRes.TabStripHeader]]><xsl:value-of select="BasissTK1"/><![CDATA[}">
                          <StackPanel>
                             <HeaderedContentControl>
                                <HeaderedContentControl.Header>
                                    <StackPanel Orientation="Horizontal" HorizontalAlignment="Right">
                                     <TextBlock Margin="2"  Text="{x:Static ]]><xsl:value-of select="TranslateTK1"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="NameTK1"/><![CDATA[}"   HorizontalAlignment="Left"/>
                                            <TextBox  Margin="10,2" Width="100" VerticalAlignment="Center" Text="{Binding Path=Search]]><xsl:value-of select="NameTK1"/><![CDATA[, UpdateSourceTrigger=PropertyChanged , Mode=TwoWay}" />
                                            <Button Content="{x:Static sharedRes:StringsRes.Search}" DockPanel.Dock="Right" Margin="10,2" VerticalAlignment="Center"
                                                    Command="{Binding Path=Get]]><xsl:value-of select="BasisTK1"/><![CDATA[ByNameCommand}" IsDefault="True" Width="120" />
                                        <TextBlock Margin="0,0,5,0">
                                                <Hyperlink Command="{Binding Path=Add]]><xsl:value-of select="BasisTK1"/><![CDATA[Command}"  >
                                                    <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkCreate}"/>
                                                </Hyperlink>
                                              | <Hyperlink Command="{Binding Path=Delete]]><xsl:value-of select="BasisTK1"/><![CDATA[Command}" >
                                                    <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkDelete}" />
                                                </Hyperlink>
                                        </TextBlock>
                                    </StackPanel>
                                </HeaderedContentControl.Header>
                                <ListView x:Name="]]><xsl:value-of select="TableTK1"/><![CDATA[List" ItemsSource="{Binding ]]><xsl:value-of select="TableTK1"/><![CDATA[List}" IsSynchronizedWithCurrentItem="True" Margin="0,0,0,0" Height="400" >
                                    <ListView.View>
                                        <GridView>
                                            <GridViewColumn DisplayMemberBinding="{Binding ]]><xsl:value-of select="IDTK1"/><![CDATA[}" Header="{x:Static ]]><xsl:value-of select="TranslateTK1"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="IDTK1"/><![CDATA[}" Width="80" />
                                            <GridViewColumn DisplayMemberBinding="{Binding ]]><xsl:value-of select="NameTK1"/><![CDATA[}" Header="{x:Static ]]><xsl:value-of select="TranslateTK1"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="NameTK1"/><![CDATA[}" Width="250" />
                                            <GridViewColumn >
                                                <GridViewColumn.Header >
                                                    <Label Content="{x:Static sharedRes:StringsRes.Valid}"/>
                                                </GridViewColumn.Header>
                                                <GridViewColumn.CellTemplate >
                                                    <DataTemplate>
                                                        <StackPanel Orientation="Horizontal" >
                                                            <CheckBox IsChecked="{Binding Path=Valid}"  />
                                                        </StackPanel>
                                                    </DataTemplate>
                                                </GridViewColumn.CellTemplate>
                                            </GridViewColumn>
                                            <GridViewColumn DisplayMemberBinding="{Binding Author}" Header="{x:Static sharedRes:StringsRes.Author}" Width="150" />
                                            <GridViewColumn DisplayMemberBinding="{Binding AuthorYear}" Header="{x:Static sharedRes:StringsRes.AuthorYear}" Width="70" />
                                        </GridView>
                                    </ListView.View>
                                </ListView>
                            </HeaderedContentControl>
                        </StackPanel>
              </TabItem>   ]]>                                            
</xsl:otherwise>    
</xsl:choose>  
                            
<xsl:choose>
<xsl:when test="Table ='++++++TK2++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">   <![CDATA[ 
                       <TabItem Header="{x:Static sharedRes:StringsRes.TabStripHeader]]><xsl:value-of select="BasissTK2"/><![CDATA[}">
                             <HeaderedContentControl>
                                <HeaderedContentControl.Header>
                                    <StackPanel Orientation="Horizontal" HorizontalAlignment="Right">
                                     <TextBlock Margin="2"  Text="{x:Static ]]><xsl:value-of select="TranslateTK2"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="NameTK2"/><![CDATA[}"   HorizontalAlignment="Left"/>
                                            <TextBox  Margin="10,2" Width="100" VerticalAlignment="Center" Text="{Binding Path=Search]]><xsl:value-of select="NameTK2"/><![CDATA[, UpdateSourceTrigger=PropertyChanged}" />
                                            <Button Content="{x:Static sharedRes:StringsRes.Search}" DockPanel.Dock="Right" Margin="10,2" VerticalAlignment="Center"
                                                    Command="{Binding Path=Get]]><xsl:value-of select="BasisTK2"/><![CDATA[ByNameCommand}" IsDefault="True" Width="120" />
                                        <TextBlock Margin="0,0,5,0">
                                                <Hyperlink Command="{Binding Path=Add]]><xsl:value-of select="BasisTK2"/><![CDATA[Command}"  >
                                                    <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkCreate}"/>
                                                </Hyperlink>
                                              | <Hyperlink Command="{Binding Path=Delete]]><xsl:value-of select="BasisTK2"/><![CDATA[Command}" >
                                                    <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkDelete}" />
                                                </Hyperlink>
                                        </TextBlock>
                                    </StackPanel>
                                </HeaderedContentControl.Header>
                                <ListView x:Name="]]><xsl:value-of select="TableTK2"/><![CDATA[List" ItemsSource="{Binding ]]><xsl:value-of select="TableTK2"/><![CDATA[List}" IsSynchronizedWithCurrentItem="True" Margin="0,0,0,0" Height="400" >
                                    <ListView.View>
                                        <GridView>
                                            <GridViewColumn DisplayMemberBinding="{Binding ]]><xsl:value-of select="IDTK2"/><![CDATA[}" Header="{x:Static ]]><xsl:value-of select="TranslateTK2"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="IDTK2"/><![CDATA[}" Width="70" />
                                            <GridViewColumn DisplayMemberBinding="{Binding ]]><xsl:value-of select="NameTK2"/><![CDATA[}" Header="{x:Static ]]><xsl:value-of select="TranslateTK2"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="NameTK2"/><![CDATA[}" Width="250" />
                                            <GridViewColumn >
                                                <GridViewColumn.Header >
                                                    <Label Content="{x:Static sharedRes:StringsRes.Valid}"/>
                                                </GridViewColumn.Header>
                                                <GridViewColumn.CellTemplate >
                                                    <DataTemplate>
                                                        <StackPanel Orientation="Horizontal" >
                                                            <CheckBox IsChecked="{Binding Path=Valid}"  />
                                                        </StackPanel>
                                                    </DataTemplate>
                                                </GridViewColumn.CellTemplate>
                                            </GridViewColumn>
                                            <GridViewColumn DisplayMemberBinding="{Binding Author}" Header="{x:Static sharedRes:StringsRes.Author}" Width="150" />
                                            <GridViewColumn DisplayMemberBinding="{Binding AuthorYear}" Header="{x:Static sharedRes:StringsRes.AuthorYear}" Width="70" />
                                        </GridView>
                                    </ListView.View>
                                </ListView>
                            </HeaderedContentControl>
                        </TabItem>   ]]>                                            
</xsl:when>  
<xsl:otherwise>      
</xsl:otherwise>    
</xsl:choose>                              
               
<xsl:choose>
<xsl:when test="Table ='Headered ContentControl TabItem Reference+++++++++++++'">        
</xsl:when>
<xsl:otherwise>          <![CDATA[  
                        <TabItem Header="{x:Static sharedRes:StringsRes.TabStripHeaderReferences}">
                            <TabControl Padding="0" Margin="1,1,1,1" SelectedIndex="{Binding SelectedMainSubTabIndex, UpdateSourceTrigger=PropertyChanged , Mode=TwoWay}" >
                                <TabItem Header="{x:Static sharedRes:StringsRes.TabStripHeaderRefExperts}">
                                    <HeaderedContentControl>
                                        <HeaderedContentControl.Header>
                                            <StackPanel Orientation="Horizontal" HorizontalAlignment="Right">
                                                <TextBlock Margin="2" HorizontalAlignment="Left">
                                                    <Label Content="{x:Static tbl90RefExpertsRes:StringsRes.RefExpertName}" />
                                                </TextBlock>
                                                <TextBox Margin="10,2" Width="100" VerticalAlignment="Center" Text="{Binding Path=SearchRefExpertName, UpdateSourceTrigger=PropertyChanged}" />
                                                <Button Content="{x:Static sharedRes:StringsRes.Search}" DockPanel.Dock="Right" Margin="10,2" VerticalAlignment="Center"
                                                            Command="{Binding Path=GetRefExpertByNameCommand}" IsDefault="True" Width="120" />
                                                <TextBlock Margin="0,0,5,0">
                                                <Hyperlink Command="{Binding Path=AddRefExpertCommand}"  >
                                                    <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkCreate}"/>
                                                </Hyperlink>
                                              | <Hyperlink Command="{Binding Path=DeleteRefExpertCommand}">
                                                    <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkDelete}"/>
                                                </Hyperlink>
                                        </TextBlock>
                                            </StackPanel>
                                        </HeaderedContentControl.Header>
                                        <ListView x:Name="Tbl90RefExpertsList" ItemsSource="{Binding Tbl90RefExpertsList}" IsSynchronizedWithCurrentItem="True" Margin="0,0,0,0" Height="380" >
                                            <ListView.View>
                                                <GridView>
                                                    <GridViewColumn DisplayMemberBinding="{Binding ReferenceID}" Header="{x:Static tbl90ReferencesRes:StringsRes.ReferenceID}" Width="80" />
                                                    <GridViewColumn DisplayMemberBinding="{Binding RefExpertID}" Header="{x:Static tbl90RefExpertsRes:StringsRes.RefExpertID}" Width="100" />
                                                    <GridViewColumn DisplayMemberBinding="{Binding Tbl90RefExperts.RefExpertName}" Header="{x:Static tbl90RefExpertsRes:StringsRes.RefExpertName}" Width="250" />
                                                    <GridViewColumn >
                                                        <GridViewColumn.Header >
                                                            <Label Content="{x:Static sharedRes:StringsRes.Valid}"/>
                                                        </GridViewColumn.Header>
                                                        <GridViewColumn.CellTemplate >
                                                            <DataTemplate>
                                                                <StackPanel Orientation="Horizontal" >
                                                                    <CheckBox IsChecked="{Binding Path=Valid}"  />
                                                                </StackPanel>
                                                            </DataTemplate>
                                                        </GridViewColumn.CellTemplate>
                                                    </GridViewColumn>
                                                    <GridViewColumn DisplayMemberBinding="{Binding Info}" Header="{x:Static sharedRes:StringsRes.Info}" Width="250" />
                                                </GridView>
                                            </ListView.View>
                                        </ListView>
                                    </HeaderedContentControl>
                                </TabItem>
                                <TabItem Header="{x:Static sharedRes:StringsRes.TabStripHeaderRefSources}">
                                    <HeaderedContentControl>
                                        <HeaderedContentControl.Header>
                                            <StackPanel Orientation="Horizontal" HorizontalAlignment="Right">
                                                <TextBlock Margin="2" HorizontalAlignment="Left">
                                                    <Label Content="{x:Static tbl90RefSourcesRes:StringsRes.RefSourceName}" />
                                                </TextBlock>
                                                <TextBox Margin="10,2" Width="100" VerticalAlignment="Center" Text="{Binding Path=SearchRefSourceName, UpdateSourceTrigger=PropertyChanged}" />
                                                <Button Content="{x:Static sharedRes:StringsRes.Search}" DockPanel.Dock="Right" Margin="10,2" VerticalAlignment="Center"
                                                            Command="{Binding Path=GetRefSourceByNameCommand}" IsDefault="True" Width="120" />
                                                <TextBlock Margin="0,0,5,0">
                                                <Hyperlink Command="{Binding Path=AddRefSourceCommand}"  >
                                                    <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkCreate}"/>
                                                </Hyperlink>
                                              | <Hyperlink Command="{Binding Path=DeleteRefSourceCommand}">
                                                    <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkDelete}"/>
                                                </Hyperlink>
                                        </TextBlock>
                                            </StackPanel>
                                        </HeaderedContentControl.Header>
                                        <ListView x:Name="Tbl90RefSourcesList" ItemsSource="{Binding Tbl90RefSourcesList}" IsSynchronizedWithCurrentItem="True" Margin="0,0,0,0" Height="380" >
                                            <ListView.View>
                                                <GridView>
                                                    <GridViewColumn DisplayMemberBinding="{Binding ReferenceID}" Header="{x:Static tbl90ReferencesRes:StringsRes.ReferenceID}" Width="80" />
                                                    <GridViewColumn DisplayMemberBinding="{Binding RefSourceID}" Header="{x:Static tbl90RefSourcesRes:StringsRes.RefSourceID}" Width="100" />
                                                    <GridViewColumn DisplayMemberBinding="{Binding Tbl90RefSources.RefSourceName}" Header="{x:Static tbl90RefSourcesRes:StringsRes.RefSourceName}" Width="450" />
                                                    <GridViewColumn >
                                                        <GridViewColumn.Header >
                                                            <Label Content="{x:Static sharedRes:StringsRes.Valid}"/>
                                                        </GridViewColumn.Header>
                                                        <GridViewColumn.CellTemplate >
                                                            <DataTemplate>
                                                                <StackPanel Orientation="Horizontal" >
                                                                    <CheckBox IsChecked="{Binding Path=Valid}"  />
                                                                </StackPanel>
                                                            </DataTemplate>
                                                        </GridViewColumn.CellTemplate>
                                                    </GridViewColumn>
                                                    <GridViewColumn DisplayMemberBinding="{Binding Info}" Header="{x:Static sharedRes:StringsRes.Info}" Width="250" />
                                                </GridView> 
                                            </ListView.View>
                                        </ListView>
                                    </HeaderedContentControl>
                                </TabItem>
                                <TabItem Header="{x:Static sharedRes:StringsRes.TabStripHeaderRefAuthors}">
                                    <HeaderedContentControl>
                                        <HeaderedContentControl.Header>
                                            <StackPanel Orientation="Horizontal" HorizontalAlignment="Right">
                                                <TextBlock Margin="2" HorizontalAlignment="Left">
                                                    <Label Content="{x:Static tbl90RefAuthorsRes:StringsRes.RefAuthorName}" />
                                                </TextBlock>
                                                <TextBox Margin="10,2" Width="100" VerticalAlignment="Center" Text="{Binding Path=SearchRefAuthorName, UpdateSourceTrigger=PropertyChanged, Mode=TwoWay}" />
                                                <Button Content="{x:Static sharedRes:StringsRes.Search}" DockPanel.Dock="Right" Margin="10,2" VerticalAlignment="Center"
                                                            Command="{Binding Path=GetRefAuthorByNameCommand}" IsDefault="True" Width="120" />
                                                <TextBlock Margin="0,0,5,0">
                                                    <Hyperlink Command="{Binding Path=AddRefAuthorCommand}"  >
                                                        <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkCreate}"/>
                                                    </Hyperlink>
                                                    | <Hyperlink Command="{Binding Path=DeleteRefAuthorCommand}">
                                                        <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkDelete}"/>
                                                    </Hyperlink>
                                                </TextBlock>
                                            </StackPanel>
                                        </HeaderedContentControl.Header>
                                        <ListView x:Name="Tbl90RefAuthorsList" ItemsSource="{Binding Tbl90RefAuthorsList}" IsSynchronizedWithCurrentItem="True" Margin="0,0,0,0" Height="380" >
                                            <ListView.View>
                                                <GridView>
                                                    <GridViewColumn DisplayMemberBinding="{Binding ReferenceID}" Header="{x:Static tbl90ReferencesRes:StringsRes.ReferenceID}" Width="80" />
                                                    <GridViewColumn DisplayMemberBinding="{Binding RefAuthorID}" Header="{x:Static tbl90RefAuthorsRes:StringsRes.RefAuthorID}" Width="100" />
                                                    <GridViewColumn DisplayMemberBinding="{Binding Tbl90RefAuthors.RefAuthorName}" Header="{x:Static tbl90RefAuthorsRes:StringsRes.RefAuthorName}" Width="550" />
                                                    <GridViewColumn >
                                                        <GridViewColumn.Header >
                                                            <Label Content="{x:Static sharedRes:StringsRes.Valid}"/>
                                                        </GridViewColumn.Header>
                                                        <GridViewColumn.CellTemplate >
                                                            <DataTemplate>
                                                                <StackPanel Orientation="Horizontal" >
                                                                    <CheckBox IsChecked="{Binding Path=Valid}"  />
                                                                </StackPanel>
                                                            </DataTemplate>
                                                        </GridViewColumn.CellTemplate>
                                                    </GridViewColumn>
                                                    <GridViewColumn DisplayMemberBinding="{Binding Info}" Header="{x:Static sharedRes:StringsRes.Info}" Width="250" />
                                                </GridView>
                                            </ListView.View>
                                        </ListView>
                                    </HeaderedContentControl>
                                </TabItem>
                            </TabControl>
                        </TabItem>
   ]]>                                            
</xsl:otherwise>    
</xsl:choose>
 
<xsl:choose>
<xsl:when test="Table ='Headered ContentControl TabItem Comments+++++++++++++'">        
</xsl:when>
<xsl:otherwise>          <![CDATA[  
                        <TabItem Header="{x:Static sharedRes:StringsRes.TabStripHeaderComments}">
                            <HeaderedContentControl>
                                <HeaderedContentControl.Header>
                                    <StackPanel Orientation="Horizontal" HorizontalAlignment="Right">
                                        <TextBlock Margin="2" HorizontalAlignment="Left">
                                                <Label Content="{x:Static sharedRes:StringsRes.Info}" />
                                        </TextBlock>
                                        <TextBox Margin="10,2" Width="100" VerticalAlignment="Center" Text="{Binding Path=SearchCommentInfo, UpdateSourceTrigger=PropertyChanged, Mode=TwoWay}" />
                                        <Button Content="{x:Static sharedRes:StringsRes.Search}" DockPanel.Dock="Right" Margin="10,2" VerticalAlignment="Center"
                                                    Command="{Binding Path=GetCommentByNameCommand}" IsDefault="True" Width="120" />
                                        <TextBlock Margin="0,0,5,0">
                                                <Hyperlink Command="{Binding Path=AddCommentCommand}"  >
                                                    <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkCreate}"/>
                                                </Hyperlink>
                                              | <Hyperlink Command="{Binding Path=DeleteCommentCommand}">
                                                    <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkDelete}"/>
                                                </Hyperlink>
                                        </TextBlock>
                                    </StackPanel>
                                </HeaderedContentControl.Header>
                                <ListView x:Name="Tbl93CommentsList" ItemsSource="{Binding Tbl93CommentsList}" IsSynchronizedWithCurrentItem="True" Margin="0,0,0,0" Height="400">
                                    <ListView.View>
                                        <GridView>
                                            <GridViewColumn DisplayMemberBinding="{Binding CommentID}" Header="{x:Static tbl93CommentsRes:StringsRes.CommentID}" Width="70" />
                                            <GridViewColumn >
                                                <GridViewColumn.Header >
                                                    <Label Content="{x:Static sharedRes:StringsRes.Valid}"/>
                                                </GridViewColumn.Header>
                                                <GridViewColumn.CellTemplate >
                                                    <DataTemplate>
                                                        <StackPanel Orientation="Horizontal" >
                                                            <CheckBox IsChecked="{Binding Path=Valid}"  />
                                                        </StackPanel>
                                                    </DataTemplate>
                                                </GridViewColumn.CellTemplate>
                                            </GridViewColumn>
                                            <GridViewColumn DisplayMemberBinding="{Binding Info}" Header="{x:Static sharedRes:StringsRes.Info}" Width="250" />
                                        </GridView>
                                    </ListView.View>
                                </ListView>
                            </HeaderedContentControl>
                        </TabItem>
   ]]>                                            
</xsl:otherwise>    
</xsl:choose>                                                                                                                                                    
                                                                                                                                                     
<xsl:choose>
<xsl:when test="Table ='HeaderedContentControl  Bottom++++++++++++++++++'"> 
</xsl:when>
<xsl:otherwise>     <![CDATA[   
                    </TabControl>
                </StackPanel>
            </StackPanel>                   
        </HeaderedContentControl>                 
   ]]>         
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='Property HeaderedContentControl   Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'"> 
</xsl:when>
<xsl:otherwise>     <![CDATA[   
        <HeaderedContentControl Style="{StaticResource SubDisplayArea}" Margin="10,0,0,0" Grid.Column="1" Header="{x:Static sharedRes:StringsRes.Properties}" Width="auto" HorizontalAlignment="Stretch" >
                <StackPanel>
   ]]>         
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='Property HeaderedContentControl  Basic Top++++++++++++++++++'"> 
</xsl:when>
<xsl:otherwise>     <![CDATA[   
                    <StackPanel>
                        <Border Style="{StaticResource DetailBorder}" Margin="5">
                            <ScrollViewer>
                                <StackPanel>
                                    <StackPanel Orientation="Horizontal" HorizontalAlignment="Right">
                                        <Button Content="{x:Static sharedRes:StringsRes.ButtonSave}" DockPanel.Dock="Right" Margin="10,2" VerticalAlignment="Center"
                                            Command="{Binding Path=Save]]><xsl:value-of select="Basis"/><![CDATA[Command}" IsDefault="True" Width="100" />
                                        <TextBlock Margin="0,0,5,0">
                                            <Hyperlink Command="{Binding Path=Add]]><xsl:value-of select="Basis"/><![CDATA[Command}"  >
                                                <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkCreate}"/>
                                            </Hyperlink>
                                        |   <Hyperlink Command="{Binding Path=Delete]]><xsl:value-of select="Basis"/><![CDATA[Command}">
                                                <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkDelete}"/>
                                            </Hyperlink>
                                        </TextBlock>
                                    </StackPanel>
                                    <StackPanel Orientation="Horizontal" >
                                        <Label Content="{x:Static ]]><xsl:value-of select="Translate"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="Name"/><![CDATA[}" Width="130" HorizontalAlignment="Left" />
                                        <TextBox Text="{Binding SelectedItem.]]><xsl:value-of select="Name"/><![CDATA[, ElementName=]]><xsl:value-of select="Table"/><![CDATA[List}" Width="200" HorizontalAlignment="Right"/>                   ]]> 
<xsl:choose>
<xsl:when test="Table ='++++++Subregnum++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">   <![CDATA[ 
                                        <Label Content="{x:Static tbl03RegnumsRes:StringsRes.Subregnum}" Width="130" HorizontalAlignment="Left" />
                                        <TextBox Text="{Binding SelectedItem.Subregnum, ElementName=Tbl03RegnumsList}" Width="200" HorizontalAlignment="Right"/>
                                    </StackPanel>  ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl06Phylums'">   <![CDATA[ 
                                    </StackPanel>  
                                    <StackPanel Orientation="Horizontal" >
                                        <Label Content="{x:Static ]]><xsl:value-of select="TranslateFK1"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[}" Width="130" HorizontalAlignment="Left" />          
                                        <ComboBox ItemsSource="{Binding ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList}" Width="535" 
                                                                  IsSynchronizedWithCurrentItem="True"  
                                                            SelectedValue="{Binding SelectedItem.]]><xsl:value-of select="IDFK1"/><![CDATA[, ElementName=]]><xsl:value-of select="Table"/><![CDATA[List, Mode=TwoWay}" 
                                                            SelectedValuePath="]]><xsl:value-of select="IDFK1"/><![CDATA[" >
                                            <ComboBox.ItemTemplate>
                                                <DataTemplate>
                                                    <StackPanel Orientation="Horizontal">
                                                        <TextBlock Text="{Binding ]]><xsl:value-of select="NameFK1"/><![CDATA[}" />
                                                           <TextBlock Text=" " />
                                                        <TextBlock Text="{Binding Subregnum}" />
                                                  </StackPanel>
                                         </DataTemplate>
                                            </ComboBox.ItemTemplate>
                                        </ComboBox>
                                </StackPanel>                 ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl09Divisions'">   <![CDATA[ 
                                    </StackPanel>  
                                    <StackPanel Orientation="Horizontal" >
                                        <Label Content="{x:Static ]]><xsl:value-of select="TranslateFK1"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[}" Width="130" HorizontalAlignment="Left" />          
                                        <ComboBox ItemsSource="{Binding ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList}" Width="535" 
                                                                  IsSynchronizedWithCurrentItem="True"  
                                                            SelectedValue="{Binding SelectedItem.]]><xsl:value-of select="IDFK1"/><![CDATA[, ElementName=]]><xsl:value-of select="Table"/><![CDATA[List, Mode=TwoWay}" 
                                                            SelectedValuePath="]]><xsl:value-of select="IDFK1"/><![CDATA[" >
                                            <ComboBox.ItemTemplate>
                                                <DataTemplate>
                                                    <StackPanel Orientation="Horizontal">
                                                        <TextBlock Text="{Binding ]]><xsl:value-of select="NameFK1"/><![CDATA[}" />
                                                           <TextBlock Text=" " />
                                                        <TextBlock Text="{Binding Subregnum}" />
                                                  </StackPanel>
                                         </DataTemplate>
                                            </ComboBox.ItemTemplate>
                                        </ComboBox>
                                </StackPanel>                 ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl18Superclasses'">   <![CDATA[ 
                                    </StackPanel>  
                                    <StackPanel Orientation="Horizontal" >
                                        <Label Content="{x:Static ]]><xsl:value-of select="TranslateFK1"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[}" Width="130" HorizontalAlignment="Left" />          
                                        <ComboBox ItemsSource="{Binding ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList}" Width="535" 
                                                                  IsSynchronizedWithCurrentItem="True"  
                                                            SelectedValue="{Binding SelectedItem.]]><xsl:value-of select="IDFK1"/><![CDATA[, ElementName=]]><xsl:value-of select="Table"/><![CDATA[List, Mode=TwoWay}" 
                                                            SelectedValuePath="]]><xsl:value-of select="IDFK1"/><![CDATA[" >
                                            <ComboBox.ItemTemplate>
                                                <DataTemplate>
                                                    <StackPanel Orientation="Horizontal">
                                                        <TextBlock Text="{Binding ]]><xsl:value-of select="NameFK1"/><![CDATA[}" />
                                                    </StackPanel>
                                                </DataTemplate>
                                            </ComboBox.ItemTemplate>
                                        </ComboBox>
                                    </StackPanel>                 
                                    <StackPanel Orientation="Horizontal" >
                                        <Label Content="{x:Static ]]><xsl:value-of select="TranslateFK2"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="NameFK2"/><![CDATA[}" Width="130" HorizontalAlignment="Left" />          
                                        <ComboBox ItemsSource="{Binding ]]><xsl:value-of select="TableFK2"/><![CDATA[AllList}" Width="535" 
                                                                  IsSynchronizedWithCurrentItem="True"  
                                                            SelectedValue="{Binding SelectedItem.]]><xsl:value-of select="IDFK2"/><![CDATA[, ElementName=]]><xsl:value-of select="Table"/><![CDATA[List, Mode=TwoWay}" 
                                                            SelectedValuePath="]]><xsl:value-of select="IDFK2"/><![CDATA[" >
                                            <ComboBox.ItemTemplate>
                                                <DataTemplate>
                                                    <StackPanel Orientation="Horizontal">
                                                        <TextBlock Text="{Binding ]]><xsl:value-of select="NameFK2"/><![CDATA[}" />
                                                    </StackPanel>
                                                </DataTemplate>
                                            </ComboBox.ItemTemplate>
                                        </ComboBox>
                                </StackPanel>                 ]]> 
</xsl:when>  
<xsl:otherwise>      <![CDATA[ 
                                    </StackPanel>  
                                    <StackPanel Orientation="Horizontal" >
                                        <Label Content="{x:Static ]]><xsl:value-of select="TranslateFK1"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[}" Width="130" HorizontalAlignment="Left" />          
                                        <ComboBox ItemsSource="{Binding ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList}" Width="535" 
                                                                  IsSynchronizedWithCurrentItem="True"  
                                                            SelectedValue="{Binding SelectedItem.]]><xsl:value-of select="IDFK1"/><![CDATA[, ElementName=]]><xsl:value-of select="Table"/><![CDATA[List, Mode=TwoWay}" 
                                                            SelectedValuePath="]]><xsl:value-of select="IDFK1"/><![CDATA[" >
                                            <ComboBox.ItemTemplate>
                                                <DataTemplate>
                                                    <StackPanel Orientation="Horizontal">
                                                        <TextBlock Text="{Binding ]]><xsl:value-of select="NameFK1"/><![CDATA[}" />
                                                    </StackPanel>
                                                </DataTemplate>
                                            </ComboBox.ItemTemplate>
                                        </ComboBox>
                                </StackPanel>                 ]]> 
</xsl:otherwise>    
</xsl:choose> <![CDATA[ 
                                <StackPanel Orientation="Horizontal" >
                                        <Label Content="{x:Static sharedRes:StringsRes.Valid}" Width="130" HorizontalAlignment="Left" />
                                        <CheckBox IsChecked="{Binding SelectedItem.Valid, ElementName=]]><xsl:value-of select="Table"/><![CDATA[List}" Width="50" HorizontalAlignment="Right"  VerticalAlignment="Center"/>
                                        <Label Content="{x:Static sharedRes:StringsRes.ValidYear}" Width="130" HorizontalAlignment="Left" />
                                    <TextBox Text="{Binding SelectedItem.ValidYear, ElementName=]]><xsl:value-of select="Table"/><![CDATA[List}" Width="50" HorizontalAlignment="Right"/>
                                </StackPanel>
                                <StackPanel Orientation="Horizontal" >
                                        <Label Content="{x:Static sharedRes:StringsRes.Synonym}" Width="130" HorizontalAlignment="Left" />
                                    <TextBox Text="{Binding SelectedItem.Synonym, ElementName=]]><xsl:value-of select="Table"/><![CDATA[List}" Width="535" HorizontalAlignment="Right"/>
                                </StackPanel>
                                <StackPanel Orientation="Horizontal" >
                                        <Label Content="{x:Static sharedRes:StringsRes.Author}" Width="130" HorizontalAlignment="Left" />
                                    <TextBox Text="{Binding SelectedItem.Author, ElementName=]]><xsl:value-of select="Table"/><![CDATA[List}" Width="200" HorizontalAlignment="Right"/>
                                        <Label Content="{x:Static sharedRes:StringsRes.AuthorYear}" Width="130" HorizontalAlignment="Left" />
                                    <TextBox Text="{Binding SelectedItem.AuthorYear, ElementName=]]><xsl:value-of select="Table"/><![CDATA[List}" Width="50" HorizontalAlignment="Right"/>
                                </StackPanel>
                                <StackPanel Orientation="Horizontal" >
                                        <Label Content="{x:Static sharedRes:StringsRes.Info}" Width="130" HorizontalAlignment="Left" />
                                    <TextBox Text="{Binding SelectedItem.Info, ElementName=]]><xsl:value-of select="Table"/><![CDATA[List}" Width="535" HorizontalAlignment="Right"/>
                                </StackPanel>
                                <StackPanel Orientation="Horizontal" >
                                        <Label Content="{x:Static sharedRes:StringsRes.EngName}" Width="130" HorizontalAlignment="Left" />
                                    <TextBox Text="{Binding SelectedItem.EngName, ElementName=]]><xsl:value-of select="Table"/><![CDATA[List}" Width="200" HorizontalAlignment="Right"/>
                                        <Label Content="{x:Static sharedRes:StringsRes.GerName}" Width="130" HorizontalAlignment="Left" />
                                    <TextBox Text="{Binding SelectedItem.GerName, ElementName=]]><xsl:value-of select="Table"/><![CDATA[List}" Width="200" HorizontalAlignment="Right"/>
                                </StackPanel>
                                <StackPanel Orientation="Horizontal" >
                                        <Label Content="{x:Static sharedRes:StringsRes.FraName}" Width="130" HorizontalAlignment="Left" />
                                    <TextBox Text="{Binding SelectedItem.FraName, ElementName=]]><xsl:value-of select="Table"/><![CDATA[List}" Width="200" HorizontalAlignment="Right"/>
                                        <Label Content="{x:Static sharedRes:StringsRes.PorName}" Width="130" HorizontalAlignment="Left" />
                                    <TextBox Text="{Binding SelectedItem.PorName, ElementName=]]><xsl:value-of select="Table"/><![CDATA[List}" Width="200" HorizontalAlignment="Right"/>
                                </StackPanel>
                                <StackPanel Orientation="Horizontal" >
                                        <Label Content="{x:Static sharedRes:StringsRes.Memo}" Width="130" HorizontalAlignment="Left" />
                                    <TextBox Text="{Binding SelectedItem.Memo, ElementName=]]><xsl:value-of select="Table"/><![CDATA[List}" Width="535" HorizontalAlignment="Right"  
                                             TextWrapping="Wrap"  AcceptsReturn="True"  VerticalScrollBarVisibility="Visible" Height="75"/>
                                </StackPanel>

                               </StackPanel>
                            </ScrollViewer>
                        </Border>
                    </StackPanel>                      ]]>         
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='Property HeaderedContentControl  Connected   FK1++++++++++++++++++'">     
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">
</xsl:when>  
<xsl:otherwise>   <![CDATA[   
                      <StackPanel>                   
                       <Border Style="{StaticResource DetailBorder}" Margin="5">
                            <TabControl Padding="0" Margin="1,1,1,1" SelectedIndex="{Binding SelectedDetailTabIndex, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}"  Height="auto" Width="auto">
                                <TabItem Header="{x:Static sharedRes:StringsRes.]]><xsl:value-of select="BasisFK1"/><![CDATA[}"  >
                                    <HeaderedContentControl>
                                        <HeaderedContentControl.Header>
                                            <StackPanel Orientation="Horizontal" HorizontalAlignment="Right">
                                                <Button Content="{x:Static sharedRes:StringsRes.ButtonSave}" DockPanel.Dock="Right" Margin="10,2" VerticalAlignment="Center"
                                                    Command="{Binding Path=Save]]><xsl:value-of select="BasisFK1"/><![CDATA[Command}" IsDefault="True" Width="100" />
                                                <TextBlock Margin="0,0,5,0">
                                                    <Hyperlink Command="{Binding Path=Add]]><xsl:value-of select="BasisFK1"/><![CDATA[Command}"  >
                                                        <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkCreate}"/>
                                                    </Hyperlink>
                                              |     <Hyperlink Command="{Binding Path=Delete]]><xsl:value-of select="BasisFK1"/><![CDATA[Command}">
                                                        <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkDelete}"/>
                                                    </Hyperlink>
                                                </TextBlock>
                                            </StackPanel>
                                        </HeaderedContentControl.Header>
                                        <StackPanel>
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static ]]><xsl:value-of select="TranslateFK1"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.]]><xsl:value-of select="NameFK1"/><![CDATA[, ElementName=]]><xsl:value-of select="TableFK1"/><![CDATA[List}" Width="520" HorizontalAlignment="Right"/>
                                            </StackPanel>
                                                         ]]> 
   <xsl:choose>
   <xsl:when test="Table ='++++++Subregnum++++++++'">
   </xsl:when>  
   <xsl:when test="Table ='Tbl06Phylums'">   <![CDATA[ 
                                    <StackPanel Orientation="Horizontal" >
                                        <Label Content="{x:Static ]]><xsl:value-of select="Translate"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="Name"/><![CDATA[}" Width="130" HorizontalAlignment="Left" />          
                                        <ComboBox ItemsSource="{Binding ]]><xsl:value-of select="Table"/><![CDATA[AllList}" Width="520" 
                                                                  IsSynchronizedWithCurrentItem="True"  
                                                            SelectedValue="{Binding SelectedItem.]]><xsl:value-of select="ID"/><![CDATA[, ElementName=]]><xsl:value-of select="TableFK1"/><![CDATA[List, Mode=TwoWay}" 
                                                            SelectedValuePath="]]><xsl:value-of select="ID"/><![CDATA[" >
                                            <ComboBox.ItemTemplate>
                                                <DataTemplate>
                                                    <StackPanel Orientation="Horizontal">
                                                        <TextBlock Text="{Binding ]]><xsl:value-of select="Name"/><![CDATA[}" />
                                                           <TextBlock Text=" " />
                                                        <TextBlock Text="{Binding Subregnum}" />
                                                  </StackPanel>
                                         </DataTemplate>
                                            </ComboBox.ItemTemplate>
                                        </ComboBox>
                                </StackPanel>                 ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl09Divisions'">   <![CDATA[ 
                                    <StackPanel Orientation="Horizontal" >
                                        <Label Content="{x:Static ]]><xsl:value-of select="Translate"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="Name"/><![CDATA[}" Width="130" HorizontalAlignment="Left" />          
                                        <ComboBox ItemsSource="{Binding ]]><xsl:value-of select="Table"/><![CDATA[AllList}" Width="520" 
                                                                  IsSynchronizedWithCurrentItem="True"  
                                                            SelectedValue="{Binding SelectedItem.]]><xsl:value-of select="ID"/><![CDATA[, ElementName=]]><xsl:value-of select="TableFK1"/><![CDATA[List, Mode=TwoWay}" 
                                                            SelectedValuePath="]]><xsl:value-of select="ID"/><![CDATA[" >
                                            <ComboBox.ItemTemplate>
                                                <DataTemplate>
                                                    <StackPanel Orientation="Horizontal">
                                                        <TextBlock Text="{Binding ]]><xsl:value-of select="Name"/><![CDATA[}" />
                                                           <TextBlock Text=" " />
                                                        <TextBlock Text="{Binding Subregnum}" />
                                                  </StackPanel>
                                         </DataTemplate>
                                            </ComboBox.ItemTemplate>
                                        </ComboBox>
                                </StackPanel>                 ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl12Subphylums'">   <![CDATA[ 
                                    <StackPanel Orientation="Horizontal" >
                                        <Label Content="{x:Static ]]><xsl:value-of select="TranslateBK1"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="NameBK1"/><![CDATA[}" Width="130" HorizontalAlignment="Left" />          
                                        <ComboBox ItemsSource="{Binding ]]><xsl:value-of select="TableBK1"/><![CDATA[AllList}" Width="520" 
                                                                  IsSynchronizedWithCurrentItem="True"  
                                                            SelectedValue="{Binding SelectedItem.]]><xsl:value-of select="IDBK1"/><![CDATA[, ElementName=]]><xsl:value-of select="TableFK1"/><![CDATA[List, Mode=TwoWay}" 
                                                            SelectedValuePath="]]><xsl:value-of select="IDBK1"/><![CDATA[" >
                                            <ComboBox.ItemTemplate>
                                                <DataTemplate>
                                                    <StackPanel Orientation="Horizontal">
                                                        <TextBlock Text="{Binding ]]><xsl:value-of select="NameBK1"/><![CDATA[}" />
                                                           <TextBlock Text=" " />
                                                        <TextBlock Text="{Binding Subregnum}" />
                                                  </StackPanel>
                                         </DataTemplate>
                                            </ComboBox.ItemTemplate>
                                        </ComboBox>
                                </StackPanel>                 ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl15Subdivisions'">   <![CDATA[ 
                                    <StackPanel Orientation="Horizontal" >
                                        <Label Content="{x:Static ]]><xsl:value-of select="TranslateBK1"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="NameBK1"/><![CDATA[}" Width="130" HorizontalAlignment="Left" />          
                                        <ComboBox ItemsSource="{Binding ]]><xsl:value-of select="TableBK1"/><![CDATA[AllList}" Width="520" 
                                                                  IsSynchronizedWithCurrentItem="True"  
                                                            SelectedValue="{Binding SelectedItem.]]><xsl:value-of select="IDBK1"/><![CDATA[, ElementName=]]><xsl:value-of select="TableFK1"/><![CDATA[List, Mode=TwoWay}" 
                                                            SelectedValuePath="]]><xsl:value-of select="IDBK1"/><![CDATA[" >
                                            <ComboBox.ItemTemplate>
                                                <DataTemplate>
                                                    <StackPanel Orientation="Horizontal">
                                                        <TextBlock Text="{Binding ]]><xsl:value-of select="NameBK1"/><![CDATA[}" />
                                                           <TextBlock Text=" " />
                                                        <TextBlock Text="{Binding Subregnum}" />
                                                  </StackPanel>
                                         </DataTemplate>
                                            </ComboBox.ItemTemplate>
                                        </ComboBox>
                                </StackPanel>                 ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl18Superclasses'">   <![CDATA[ 
                                    <StackPanel Orientation="Horizontal" >
                                        <Label Content="{x:Static ]]><xsl:value-of select="TranslateBK1"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="NameBK1"/><![CDATA[}" Width="130" HorizontalAlignment="Left" />          
                                        <ComboBox ItemsSource="{Binding ]]><xsl:value-of select="TableBK1"/><![CDATA[AllList}" Width="520" 
                                                                  IsSynchronizedWithCurrentItem="True"  
                                                            SelectedValue="{Binding SelectedItem.]]><xsl:value-of select="IDBK1"/><![CDATA[, ElementName=]]><xsl:value-of select="TableFK1"/><![CDATA[List, Mode=TwoWay}" 
                                                            SelectedValuePath="]]><xsl:value-of select="IDBK1"/><![CDATA[" >
                                            <ComboBox.ItemTemplate>
                                                <DataTemplate>
                                                    <StackPanel Orientation="Horizontal">
                                                        <TextBlock Text="{Binding ]]><xsl:value-of select="NameBK1"/><![CDATA[}" />
                                                           <TextBlock Text=" " />
                                                        <TextBlock Text="{Binding Subregnum}" />
                                                  </StackPanel>
                                         </DataTemplate>
                                            </ComboBox.ItemTemplate>
                                        </ComboBox>
                                </StackPanel>                 ]]> 
</xsl:when>  
<xsl:otherwise>      <![CDATA[ 
                                    <StackPanel Orientation="Horizontal" >
                                        <Label Content="{x:Static ]]><xsl:value-of select="TranslateFK1"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="NameFK1"/><![CDATA[}" Width="130" HorizontalAlignment="Left" />          
                                        <ComboBox ItemsSource="{Binding ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList}" Width="520" 
                                                                  IsSynchronizedWithCurrentItem="True"  
                                                            SelectedValue="{Binding SelectedItem.]]><xsl:value-of select="IDFK1"/><![CDATA[, ElementName=]]><xsl:value-of select="Table"/><![CDATA[List, Mode=TwoWay}" 
                                                            SelectedValuePath="]]><xsl:value-of select="IDFK1"/><![CDATA[" >
                                            <ComboBox.ItemTemplate>
                                                <DataTemplate>
                                                    <StackPanel Orientation="Horizontal">
                                                        <TextBlock Text="{Binding ]]><xsl:value-of select="NameFK1"/><![CDATA[}" />
                                                    </StackPanel>
                                                </DataTemplate>
                                            </ComboBox.ItemTemplate>
                                        </ComboBox>
                                </StackPanel>                 ]]> 
</xsl:otherwise>    
</xsl:choose> <![CDATA[ 
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.Valid}" Width="130" HorizontalAlignment="Left" />
                                                <CheckBox IsChecked="{Binding SelectedItem.Valid, ElementName=]]><xsl:value-of select="TableFK1"/><![CDATA[List}" Width="50" HorizontalAlignment="Right"  VerticalAlignment="Center"/>
                                                <Label Content="{x:Static sharedRes:StringsRes.ValidYear}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.ValidYear, ElementName=]]><xsl:value-of select="TableFK1"/><![CDATA[List}" Width="50" HorizontalAlignment="Right"/>
                                            </StackPanel>
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.Synonym}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.Synonym, ElementName=]]><xsl:value-of select="TableFK1"/><![CDATA[List}" Width="520" HorizontalAlignment="Right"/>
                                            </StackPanel>
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.Author}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.Author, ElementName=]]><xsl:value-of select="TableFK1"/><![CDATA[List}" Width="200" HorizontalAlignment="Right"/>
                                                <Label Content="{x:Static sharedRes:StringsRes.AuthorYear}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.AuthorYear, ElementName=]]><xsl:value-of select="TableFK1"/><![CDATA[List}" Width="50" HorizontalAlignment="Right"/>
                                            </StackPanel>
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.Info}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.Info, ElementName=]]><xsl:value-of select="TableFK1"/><![CDATA[List}" Width="520" HorizontalAlignment="Right"/>
                                            </StackPanel>
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.EngName}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.EngName, ElementName=]]><xsl:value-of select="TableFK1"/><![CDATA[List}" Width="200" HorizontalAlignment="Right"/>
                                                <Label Content="{x:Static sharedRes:StringsRes.GerName}" Width="115" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.GerName, ElementName=]]><xsl:value-of select="TableFK1"/><![CDATA[List}" Width="200" HorizontalAlignment="Right"/>
                                            </StackPanel>
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.FraName}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.FraName, ElementName=]]><xsl:value-of select="TableFK1"/><![CDATA[List}" Width="200" HorizontalAlignment="Right"/>
                                                <Label Content="{x:Static sharedRes:StringsRes.PorName}" Width="115" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.PorName, ElementName=]]><xsl:value-of select="TableFK1"/><![CDATA[List}" Width="200" HorizontalAlignment="Right"/>
                                            </StackPanel>
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.Memo}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.Memo, ElementName=]]><xsl:value-of select="TableFK1"/><![CDATA[List}" Width="520" HorizontalAlignment="Right"  
                                                     TextWrapping="Wrap"  AcceptsReturn="True"  VerticalScrollBarVisibility="Visible" Height="50"/>
                                            </StackPanel>
                                        </StackPanel>
                                    </HeaderedContentControl>
                                </TabItem>  ]]>       
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='++++++Tabitem FK2++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl18Superclasses'">   <![CDATA[   
                                <TabItem Header="{x:Static sharedRes:StringsRes.]]><xsl:value-of select="BasisFK2"/><![CDATA[}"  >
                                    <HeaderedContentControl>
                                        <HeaderedContentControl.Header>
                                            <StackPanel Orientation="Horizontal" HorizontalAlignment="Right">
                                                <Button Content="{x:Static sharedRes:StringsRes.ButtonSave}" DockPanel.Dock="Right" Margin="10,2" VerticalAlignment="Center"
                                                    Command="{Binding Path=Save]]><xsl:value-of select="BasisFK2"/><![CDATA[Command}" IsDefault="True" Width="100" />
                                                <TextBlock Margin="0,0,5,0">
                                                    <Hyperlink Command="{Binding Path=Add]]><xsl:value-of select="BasisFK2"/><![CDATA[Command}"  >
                                                        <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkCreate}"/>
                                                    </Hyperlink>
                                              |     <Hyperlink Command="{Binding Path=Delete]]><xsl:value-of select="BasisFK2"/><![CDATA[Command}">
                                                        <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkDelete}"/>
                                                    </Hyperlink>
                                                </TextBlock>
                                            </StackPanel>
                                        </HeaderedContentControl.Header>
                                        <StackPanel>
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static ]]><xsl:value-of select="TranslateFK2"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="NameFK2"/><![CDATA[}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.]]><xsl:value-of select="NameFK2"/><![CDATA[, ElementName=]]><xsl:value-of select="TableFK2"/><![CDATA[List}" Width="520" HorizontalAlignment="Right"/>
                                            </StackPanel>
                                    <StackPanel Orientation="Horizontal" >
                                        <Label Content="{x:Static ]]><xsl:value-of select="TranslateBK2"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="NameBK2"/><![CDATA[}" Width="130" HorizontalAlignment="Left" />          
                                        <ComboBox ItemsSource="{Binding ]]><xsl:value-of select="TableBK2"/><![CDATA[AllList}" Width="520" 
                                                                  IsSynchronizedWithCurrentItem="True"  
                                                            SelectedValue="{Binding SelectedItem.]]><xsl:value-of select="IDBK2"/><![CDATA[, ElementName=]]><xsl:value-of select="TableFK2"/><![CDATA[List, Mode=TwoWay}" 
                                                            SelectedValuePath="]]><xsl:value-of select="IDBK2"/><![CDATA[" >
                                            <ComboBox.ItemTemplate>
                                                <DataTemplate>
                                                    <StackPanel Orientation="Horizontal">
                                                        <TextBlock Text="{Binding ]]><xsl:value-of select="NameBK2"/><![CDATA[}" />
                                                           <TextBlock Text=" " />
                                                        <TextBlock Text="{Binding Subregnum}" />
                                                  </StackPanel>
                                         </DataTemplate>
                                            </ComboBox.ItemTemplate>
                                        </ComboBox>
                                </StackPanel>                
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.Valid}" Width="130" HorizontalAlignment="Left" />
                                                <CheckBox IsChecked="{Binding SelectedItem.Valid, ElementName=]]><xsl:value-of select="TableFK2"/><![CDATA[List}" Width="50" HorizontalAlignment="Right"  VerticalAlignment="Center"/>
                                                <Label Content="{x:Static sharedRes:StringsRes.ValidYear}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.ValidYear, ElementName=]]><xsl:value-of select="TableFK2"/><![CDATA[List}" Width="50" HorizontalAlignment="Right"/>
                                            </StackPanel>
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.Synonym}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.Synonym, ElementName=]]><xsl:value-of select="TableFK2"/><![CDATA[List}" Width="520" HorizontalAlignment="Right"/>
                                            </StackPanel>
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.Author}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.Author, ElementName=]]><xsl:value-of select="TableFK2"/><![CDATA[List}" Width="200" HorizontalAlignment="Right"/>
                                                <Label Content="{x:Static sharedRes:StringsRes.AuthorYear}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.AuthorYear, ElementName=]]><xsl:value-of select="TableFK2"/><![CDATA[List}" Width="50" HorizontalAlignment="Right"/>
                                            </StackPanel>
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.Info}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.Info, ElementName=]]><xsl:value-of select="TableFK2"/><![CDATA[List}" Width="520" HorizontalAlignment="Right"/>
                                            </StackPanel>
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.EngName}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.EngName, ElementName=]]><xsl:value-of select="TableFK2"/><![CDATA[List}" Width="200" HorizontalAlignment="Right"/>
                                                <Label Content="{x:Static sharedRes:StringsRes.GerName}" Width="115" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.GerName, ElementName=]]><xsl:value-of select="TableFK2"/><![CDATA[List}" Width="200" HorizontalAlignment="Right"/>
                                            </StackPanel>
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.FraName}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.FraName, ElementName=]]><xsl:value-of select="TableFK2"/><![CDATA[List}" Width="200" HorizontalAlignment="Right"/>
                                                <Label Content="{x:Static sharedRes:StringsRes.PorName}" Width="115" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.PorName, ElementName=]]><xsl:value-of select="TableFK2"/><![CDATA[List}" Width="200" HorizontalAlignment="Right"/>
                                            </StackPanel>
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.Memo}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.Memo, ElementName=]]><xsl:value-of select="TableFK2"/><![CDATA[List}" Width="520" HorizontalAlignment="Right"  
                                                     TextWrapping="Wrap"  AcceptsReturn="True"  VerticalScrollBarVisibility="Visible" Height="50"/>
                                            </StackPanel>
                                        </StackPanel>
                                    </HeaderedContentControl>
                                </TabItem>  ]]>                                                                
</xsl:when>  
<xsl:otherwise>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++tabcontrol++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">   <![CDATA[ 
                    <StackPanel>
                        <Border Style="{StaticResource DetailBorder}" Margin="5">
                            <TabControl Padding="0" Margin="1,1,1,1" SelectedIndex="{Binding SelectedDetailTabIndex, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}" Width="auto" Height="auto" Width="auto" >   ]]>
</xsl:when>  
<xsl:otherwise>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Property HeaderedContentControl  Connected   TK1++++++++++++++++++'">     
</xsl:when>
<xsl:otherwise> 
  <xsl:if test="TableTK1 !='NULL'">       <![CDATA[ 
                                <TabItem Header="{x:Static sharedRes:StringsRes.]]><xsl:value-of select="BasisTK1"/><![CDATA[}">
                                    <HeaderedContentControl>
                                        <HeaderedContentControl.Header>
                                            <StackPanel Orientation="Horizontal" HorizontalAlignment="Right">
                                                <Button Content="{x:Static sharedRes:StringsRes.ButtonSave}" DockPanel.Dock="Right" Margin="10,2" VerticalAlignment="Center"
                                                    Command="{Binding Path=Save]]><xsl:value-of select="BasisTK1"/><![CDATA[Command}" IsDefault="True" Width="100" />
                                                <TextBlock Margin="0,0,5,0">
                                                <Hyperlink Command="{Binding Path=Add]]><xsl:value-of select="BasisTK1"/><![CDATA[Command}"  >
                                                    <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkCreate}"/>
                                                </Hyperlink>
                                              | <Hyperlink Command="{Binding Path=Delete]]><xsl:value-of select="BasisTK1"/><![CDATA[Command}">
                                                    <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkDelete}"/>
                                                </Hyperlink>
                                        </TextBlock>
                                            </StackPanel>
                                        </HeaderedContentControl.Header>
                                        <StackPanel>  ]]>
<xsl:choose>
<xsl:when test="Table ='+++++Subregnum++++++++++++++++++'"> 
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">   <![CDATA[   
                                             <StackPanel Orientation="Horizontal" >
                                                 <Label Content="{x:Static ]]><xsl:value-of select="TranslateTK1"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="NameTK1"/><![CDATA[}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.]]><xsl:value-of select="NameTK1"/><![CDATA[, ElementName=]]><xsl:value-of select="TableTK1"/><![CDATA[List}" Width="520" HorizontalAlignment="Right"/>
                                            </StackPanel>  
                                           <StackPanel Orientation="Horizontal" >
                                                   <Label Content="{x:Static ]]><xsl:value-of select="Translate"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="Name"/><![CDATA[}" Width="130" HorizontalAlignment="Left" />
                                                   <ComboBox ItemsSource="{Binding ]]><xsl:value-of select="Table"/><![CDATA[AllList}" Width="520" 
                                                                  IsSynchronizedWithCurrentItem="True"  
                                                            SelectedValue="{Binding SelectedItem.]]><xsl:value-of select="ID"/><![CDATA[, ElementName=]]><xsl:value-of select="TableTK1"/><![CDATA[List, Mode=TwoWay}" 
                                                            SelectedValuePath="]]><xsl:value-of select="ID"/><![CDATA[" >
                                                    <ComboBox.ItemTemplate>
                                                        <DataTemplate>
                                                            <StackPanel Orientation="Horizontal">
                                                                <TextBlock Text="{Binding ]]><xsl:value-of select="Name"/><![CDATA[}" />
                                                                   <TextBlock Text=" " />
                                                                <TextBlock Text="{Binding Subregnum}" />
                                                         </StackPanel>
                                                        </DataTemplate>
                                                    </ComboBox.ItemTemplate>
                                                </ComboBox>
                                            </StackPanel>      ]]>
</xsl:when>
<xsl:otherwise>         <![CDATA[ 
                                             <StackPanel Orientation="Horizontal" >
                                                 <Label Content="{x:Static ]]><xsl:value-of select="TranslateTK1"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="NameTK1"/><![CDATA[}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.]]><xsl:value-of select="NameTK1"/><![CDATA[, ElementName=]]><xsl:value-of select="TableTK1"/><![CDATA[List}" Width="520" HorizontalAlignment="Right"/>
                                            </StackPanel>  
                                           <StackPanel Orientation="Horizontal" >
                                                   <Label Content="{x:Static ]]><xsl:value-of select="Translate"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="Name"/><![CDATA[}" Width="130" HorizontalAlignment="Left" />
                                                   <ComboBox ItemsSource="{Binding ]]><xsl:value-of select="Table"/><![CDATA[AllList}" Width="520" 
                                                                  IsSynchronizedWithCurrentItem="True"  
                                                            SelectedValue="{Binding SelectedItem.]]><xsl:value-of select="ID"/><![CDATA[, ElementName=]]><xsl:value-of select="TableTK1"/><![CDATA[List, Mode=TwoWay}" 
                                                            SelectedValuePath="]]><xsl:value-of select="ID"/><![CDATA[" >
                                                    <ComboBox.ItemTemplate>
                                                        <DataTemplate>
                                                            <StackPanel Orientation="Horizontal">
                                                                <TextBlock Text="{Binding ]]><xsl:value-of select="Name"/><![CDATA[}" />
                                                         </StackPanel>
                                                        </DataTemplate>
                                                    </ComboBox.ItemTemplate>
                                                </ComboBox>
                                            </StackPanel>      ]]>
</xsl:otherwise>    
</xsl:choose>               <![CDATA[ 
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.Valid}" Width="130" HorizontalAlignment="Left" />
                                                <CheckBox IsChecked="{Binding SelectedItem.Valid, ElementName=]]><xsl:value-of select="TableTK1"/><![CDATA[List}" Width="50" HorizontalAlignment="Right"  VerticalAlignment="Center"/>
                                                <Label Content="{x:Static sharedRes:StringsRes.ValidYear}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.ValidYear, ElementName=]]><xsl:value-of select="TableTK1"/><![CDATA[List}" Width="50" HorizontalAlignment="Right"/>
                                            </StackPanel>
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.Synonym}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.Synonym, ElementName=]]><xsl:value-of select="TableTK1"/><![CDATA[List}" Width="520" HorizontalAlignment="Right"/>
                                            </StackPanel>
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.Author}"  Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.Author, ElementName=]]><xsl:value-of select="TableTK1"/><![CDATA[List}" Width="200" HorizontalAlignment="Right"/>
                                                <Label Content="{x:Static sharedRes:StringsRes.AuthorYear}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.AuthorYear, ElementName=]]><xsl:value-of select="TableTK1"/><![CDATA[List}" Width="50" HorizontalAlignment="Right"/>
                                            </StackPanel>
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.Info}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.Info, ElementName=]]><xsl:value-of select="TableTK1"/><![CDATA[List}" Width="520" HorizontalAlignment="Right"/>
                                            </StackPanel>
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.EngName}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.EngName, ElementName=]]><xsl:value-of select="TableTK1"/><![CDATA[List}" Width="200" HorizontalAlignment="Right"/>
                                                <Label Content="{x:Static sharedRes:StringsRes.GerName}" Width="115" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.GerName, ElementName=]]><xsl:value-of select="TableTK1"/><![CDATA[List}" Width="200" HorizontalAlignment="Right"/>
                                            </StackPanel>
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.FraName}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.FraName, ElementName=]]><xsl:value-of select="TableTK1"/><![CDATA[List}" Width="200" HorizontalAlignment="Right"/>
                                                <Label Content="{x:Static sharedRes:StringsRes.PorName}" Width="115" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.PorName, ElementName=]]><xsl:value-of select="TableTK1"/><![CDATA[List}" Width="200" HorizontalAlignment="Right"/>
                                            </StackPanel>
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.Memo}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.Memo, ElementName=]]><xsl:value-of select="TableTK1"/><![CDATA[List}" Width="520" HorizontalAlignment="Right"  
                                                     TextWrapping="Wrap"  AcceptsReturn="True"  VerticalScrollBarVisibility="Visible" Height="50"/>
                                            </StackPanel>
                                        </StackPanel>
                                    </HeaderedContentControl>
                                </TabItem>
  ]]> 
  </xsl:if>        
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='Property HeaderedContentControl  Connected   TK2++++++++++++++++++'">     
</xsl:when>
<xsl:otherwise> 
  <xsl:if test="TableTK2 !='NULL'">       <![CDATA[ 
                      <StackPanel>                   
                       <Border Style="{StaticResource DetailBorder}" Margin="5">
                            <TabControl Padding="0" Margin="1,1,1,1" SelectedIndex="{Binding SelectedDetailTabIndex, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}">
                                <TabItem Header="{x:Static sharedRes:StringsRes.]]><xsl:value-of select="BasisTK2"/><![CDATA[}">
                                    <HeaderedContentControl>
                                        <HeaderedContentControl.Header>
                                            <StackPanel Orientation="Horizontal" HorizontalAlignment="Right">
                                                <Button Content="{x:Static sharedRes:StringsRes.ButtonSave}" DockPanel.Dock="Right" Margin="10,2" VerticalAlignment="Center"
                                                    Command="{Binding Path=Save]]><xsl:value-of select="BasisTK2"/><![CDATA[Command}" IsDefault="True" Width="100" />
                                                <TextBlock Margin="0,0,5,0">
                                                <Hyperlink Command="{Binding Path=Add]]><xsl:value-of select="BasisTK2"/><![CDATA[Command}"  >
                                                    <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkCreate}"/>
                                                </Hyperlink>
                                              | <Hyperlink Command="{Binding Path=Delete]]><xsl:value-of select="BasisTK2"/><![CDATA[Command}">
                                                    <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkDelete}"/>
                                                </Hyperlink>
                                        </TextBlock>
                                            </StackPanel>
                                        </HeaderedContentControl.Header>
                                        <StackPanel>  ]]>
<xsl:choose>
<xsl:when test="Table ='+++++Subregnum++++++++++++++++++'"> 
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">   <![CDATA[   
                                             <StackPanel Orientation="Horizontal" >
                                                 <Label Content="{x:Static ]]><xsl:value-of select="TranslateTK2"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="NameTK2"/><![CDATA[}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.]]><xsl:value-of select="NameTK2"/><![CDATA[, ElementName=]]><xsl:value-of select="TableTK2"/><![CDATA[List}" Width="520" HorizontalAlignment="Right"/>
                                            </StackPanel>  
                                           <StackPanel Orientation="Horizontal" >
                                                   <Label Content="{x:Static ]]><xsl:value-of select="Translate"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="Name"/><![CDATA[}" Width="130" HorizontalAlignment="Left" />
                                                   <ComboBox ItemsSource="{Binding ]]><xsl:value-of select="Table"/><![CDATA[AllList}" Width="520" 
                                                                  IsSynchronizedWithCurrentItem="True"  
                                                            SelectedValue="{Binding SelectedItem.]]><xsl:value-of select="ID"/><![CDATA[, ElementName=]]><xsl:value-of select="TableTK2"/><![CDATA[List, Mode=TwoWay}" 
                                                            SelectedValuePath="]]><xsl:value-of select="ID"/><![CDATA[" >
                                                    <ComboBox.ItemTemplate>
                                                        <DataTemplate>
                                                            <StackPanel Orientation="Horizontal">
                                                                <TextBlock Text="{Binding ]]><xsl:value-of select="Name"/><![CDATA[}" />
                                                                   <TextBlock Text=" " />
                                                                <TextBlock Text="{Binding Subregnum}" />
                                                         </StackPanel>
                                                        </DataTemplate>
                                                    </ComboBox.ItemTemplate>
                                                </ComboBox>
                                            </StackPanel>      ]]>
</xsl:when>
<xsl:otherwise>         <![CDATA[ 
                                             <StackPanel Orientation="Horizontal" >
                                                   <Label Content="{x:Static ]]><xsl:value-of select="Translate"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="Name"/><![CDATA[}" Width="130" HorizontalAlignment="Left" />
                                                   <ComboBox ItemsSource="{Binding ]]><xsl:value-of select="Table"/><![CDATA[AllList}" Width="520" 
                                                                  IsSynchronizedWithCurrentItem="True"  
                                                            SelectedValue="{Binding SelectedItem.]]><xsl:value-of select="ID"/><![CDATA[, ElementName=]]><xsl:value-of select="TableTK2"/><![CDATA[List, Mode=TwoWay}" 
                                                            SelectedValuePath="]]><xsl:value-of select="ID"/><![CDATA[" >
                                                    <ComboBox.ItemTemplate>
                                                        <DataTemplate>
                                                            <StackPanel Orientation="Horizontal">
                                                                <TextBlock Text="{Binding ]]><xsl:value-of select="Name"/><![CDATA[}" />
                                                            </StackPanel>
                                                        </DataTemplate>
                                                    </ComboBox.ItemTemplate>
                                                </ComboBox>
                                            </StackPanel>  ]]>
</xsl:otherwise>    
</xsl:choose>               <![CDATA[ 
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.Valid}" Width="130" HorizontalAlignment="Left" />
                                                <CheckBox IsChecked="{Binding SelectedItem.Valid, ElementName=]]><xsl:value-of select="TableTK2"/><![CDATA[List}" Width="50" HorizontalAlignment="Right"  VerticalAlignment="Center"/>
                                                <Label Content="{x:Static sharedRes:StringsRes.ValidYear}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.ValidYear, ElementName=]]><xsl:value-of select="TableTK2"/><![CDATA[List}" Width="50" HorizontalAlignment="Right"/>
                                            </StackPanel>
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.Synonym}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.Synonym, ElementName=]]><xsl:value-of select="TableTK2"/><![CDATA[List}" Width="520" HorizontalAlignment="Right"/>
                                            </StackPanel>
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.Author}"  Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.Author, ElementName=]]><xsl:value-of select="TableTK2"/><![CDATA[List}" Width="200" HorizontalAlignment="Right"/>
                                                <Label Content="{x:Static sharedRes:StringsRes.AuthorYear}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.AuthorYear, ElementName=]]><xsl:value-of select="TableTK2"/><![CDATA[List}" Width="50" HorizontalAlignment="Right"/>
                                            </StackPanel>
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.Info}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.Info, ElementName=]]><xsl:value-of select="TableTK2"/><![CDATA[List}" Width="520" HorizontalAlignment="Right"/>
                                            </StackPanel>
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.EngName}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.EngName, ElementName=]]><xsl:value-of select="TableTK2"/><![CDATA[List}" Width="200" HorizontalAlignment="Right"/>
                                                <Label Content="{x:Static sharedRes:StringsRes.GerName}" Width="115" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.GerName, ElementName=]]><xsl:value-of select="TableTK2"/><![CDATA[List}" Width="200" HorizontalAlignment="Right"/>
                                            </StackPanel>
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.FraName}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.FraName, ElementName=]]><xsl:value-of select="TableTK2"/><![CDATA[List}" Width="200" HorizontalAlignment="Right"/>
                                                <Label Content="{x:Static sharedRes:StringsRes.PorName}" Width="115" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.PorName, ElementName=]]><xsl:value-of select="TableTK2"/><![CDATA[List}" Width="200" HorizontalAlignment="Right"/>
                                            </StackPanel>
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.Memo}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.Memo, ElementName=]]><xsl:value-of select="TableTK2"/><![CDATA[List}" Width="520" HorizontalAlignment="Right"  
                                                     TextWrapping="Wrap"  AcceptsReturn="True"  VerticalScrollBarVisibility="Visible" Height="Width="75""/>
                                            </StackPanel>
                                        </StackPanel>
                                    </HeaderedContentControl>
                                </TabItem>
  ]]> 
  </xsl:if>        
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='Property HeaderedContentControl  Connected   Reference and Comment++++++++++++++++++'">     
</xsl:when>
<xsl:otherwise>    <![CDATA[   
                                <TabItem Header="{x:Static sharedRes:StringsRes.TabStripHeaderReferences}">      
                                    <TabControl Padding="0" Margin="1,1,1,1" SelectedIndex="{Binding SelectedDetailSubTabIndex, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}" >
                                        <TabItem Header="{x:Static sharedRes:StringsRes.ReferenceExpert}">
                                            <HeaderedContentControl>
                                                <HeaderedContentControl.Header>
                                                    <StackPanel Orientation="Horizontal" HorizontalAlignment="Right">
                                                        <Button Content="{x:Static sharedRes:StringsRes.ButtonSave}" DockPanel.Dock="Right" Margin="10,2" VerticalAlignment="Center"
                                                            Command="{Binding Path=SaveRefExpertCommand}" IsDefault="True" Width="100" />
                                                        <TextBlock Margin="0,0,5,0">
                                                            <Hyperlink Command="{Binding Path=AddRefExpertCommand}"  >
                                                                <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkCreate}"/>
                                                            </Hyperlink>
                                                          | <Hyperlink Command="{Binding Path=DeleteRefExpertCommand}">
                                                                <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkDelete}"/>
                                                            </Hyperlink>
                                                        </TextBlock>
                                                    </StackPanel>
                                                </HeaderedContentControl.Header>
                                            <StackPanel>   ]]>
<xsl:choose>
<xsl:when test="Table ='+++++Subregnum++++++++++++++++++'"> 
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">   <![CDATA[   
                                             <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static ]]><xsl:value-of select="Translate"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="Name"/><![CDATA[}" Width="130" HorizontalAlignment="Left" />
                                                   <ComboBox ItemsSource="{Binding ]]><xsl:value-of select="Table"/><![CDATA[AllList}" Width="520" 
                                                                  IsSynchronizedWithCurrentItem="True"  
                                                            SelectedValue="{Binding SelectedItem.]]><xsl:value-of select="ID"/><![CDATA[, ElementName=Tbl90RefExpertsList, Mode=TwoWay}" 
                                                            SelectedValuePath="]]><xsl:value-of select="ID"/><![CDATA[" >
                                                    <ComboBox.ItemTemplate>
                                                        <DataTemplate>
                                                            <StackPanel Orientation="Horizontal">
                                                                <TextBlock Text="{Binding ]]><xsl:value-of select="Name"/><![CDATA[}" />
                                                                   <TextBlock Text=" " />
                                                                <TextBlock Text="{Binding Subregnum}" />
                                                         </StackPanel>
                                                        </DataTemplate>
                                                    </ComboBox.ItemTemplate>
                                                </ComboBox>
                                            </StackPanel>      ]]>
</xsl:when>
<xsl:otherwise>     
  <![CDATA[                                           <StackPanel Orientation="Horizontal" >
                                                        <Label Content="{x:Static ]]><xsl:value-of select="Translate"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="Name"/><![CDATA[}" Width="130" HorizontalAlignment="Left" />
                                                        <ComboBox ItemsSource="{Binding ]]><xsl:value-of select="Table"/><![CDATA[AllList}" Width="520" 
                                                                  IsSynchronizedWithCurrentItem="True"  
                                                            SelectedValue="{Binding SelectedItem.]]><xsl:value-of select="ID"/><![CDATA[, ElementName=Tbl90RefExpertsList, Mode=TwoWay}" 
                                                            SelectedValuePath="]]><xsl:value-of select="ID"/><![CDATA[" >
                                                            <ComboBox.ItemTemplate>
                                                                <DataTemplate>
                                                                    <StackPanel Orientation="Horizontal">
                                                                        <TextBlock Text="{Binding ]]><xsl:value-of select="Name"/><![CDATA[}" />
                                                                    </StackPanel>
                                                                </DataTemplate>
                                                            </ComboBox.ItemTemplate>
                                                        </ComboBox>
                                            </StackPanel>  ]]>
</xsl:otherwise>    
</xsl:choose>

<![CDATA[                                                     <StackPanel Orientation="Horizontal" >
                                                        <Label Content="{x:Static tbl90RefExpertsRes:StringsRes.RefExpertName}" Width="130" HorizontalAlignment="Left" />
                                                        <ComboBox ItemsSource="{Binding Tbl90ExpertsAllList}" Width="520" 
                                                                  IsSynchronizedWithCurrentItem="True" 
                                                            SelectedValue="{Binding SelectedItem.RefExpertID, ElementName=Tbl90RefExpertsList, Mode=TwoWay}" 
                                                            SelectedValuePath="RefExpertID" 
                                                            DisplayMemberPath="RefExpertName"   />
                                                    </StackPanel>
                                                    <StackPanel Orientation="Horizontal" >
                                                        <Label Content="{x:Static sharedRes:StringsRes.Valid}" Width="130" HorizontalAlignment="Left" />
                                                        <CheckBox IsChecked="{Binding SelectedItem.Valid, ElementName=Tbl90RefExpertsList}" Width="50" HorizontalAlignment="Right"  VerticalAlignment="Center"/>
                                                        <Label Content="{x:Static sharedRes:StringsRes.ValidYear}" Width="130" HorizontalAlignment="Left" />
                                                        <TextBox Text="{Binding SelectedItem.ValidYear, ElementName=Tbl90RefExpertsList}" Width="50" HorizontalAlignment="Right"/>
                                                    </StackPanel>
                                                    <StackPanel Orientation="Horizontal" >
                                                        <Label Content="{x:Static sharedRes:StringsRes.ReportNotes}" Width="130" HorizontalAlignment="Left" />
                                                        <TextBox Text="{Binding SelectedItem.Notes, ElementName=Tbl90RefExpertsList}" Width="520" HorizontalAlignment="Right"  
                                                             TextWrapping="Wrap"  AcceptsReturn="True"  VerticalScrollBarVisibility="Visible" Height="75"/>
                                                    </StackPanel>
                                                    <StackPanel Orientation="Horizontal" >
                                                        <Label Content="{x:Static sharedRes:StringsRes.Info}" Width="130" HorizontalAlignment="Left" />
                                                        <TextBox Text="{Binding SelectedItem.Info, ElementName=Tbl90RefExpertsList}" Width="520" HorizontalAlignment="Right"/>
                                                    </StackPanel>
                                                    <StackPanel Orientation="Horizontal" >
                                                        <Label Content="{x:Static sharedRes:StringsRes.Memo}" Width="130" HorizontalAlignment="Left" />
                                                        <TextBox Text="{Binding SelectedItem.Memo, ElementName=Tbl90RefExpertsList}" Width="520" HorizontalAlignment="Right"  
                                                             TextWrapping="Wrap"  AcceptsReturn="True"  VerticalScrollBarVisibility="Visible" Height="50"/>
                                                    </StackPanel>
                                                </StackPanel>
                                            </HeaderedContentControl>
                                        </TabItem>
                                        <TabItem Header="{x:Static sharedRes:StringsRes.ReferenceSource}">
                                            <HeaderedContentControl>
                                                <HeaderedContentControl.Header>
                                                    <StackPanel Orientation="Horizontal" HorizontalAlignment="Right">
                                                        <Button Content="{x:Static sharedRes:StringsRes.ButtonSave}" DockPanel.Dock="Right" Margin="10,2" VerticalAlignment="Center"
                                                            Command="{Binding Path=SaveRefSourceCommand}" IsDefault="True" Width="100" />
                                                        <TextBlock Margin="0,0,5,0">
                                                        <Hyperlink Command="{Binding Path=AddRefSourceCommand}"  >
                                                            <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkCreate}"/>
                                                        </Hyperlink>
                                                        | <Hyperlink Command="{Binding Path=DeleteRefSourceCommand}">
                                                            <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkDelete}"/>
                                                        </Hyperlink>
                                                    </TextBlock>
                                             </StackPanel>
                                             </HeaderedContentControl.Header>   
                                               <StackPanel>   ]]>
<xsl:choose>
<xsl:when test="Table ='+++++Subregnum++++++++++++++++++'"> 
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">   <![CDATA[   
                                             <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static ]]><xsl:value-of select="Translate"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="Name"/><![CDATA[}" Width="130" HorizontalAlignment="Left" />
                                                   <ComboBox ItemsSource="{Binding ]]><xsl:value-of select="Table"/><![CDATA[AllList}" Width="620" 
                                                                  IsSynchronizedWithCurrentItem="True"  
                                                            SelectedValue="{Binding SelectedItem.]]><xsl:value-of select="ID"/><![CDATA[, ElementName=Tbl90RefSourcesList, Mode=TwoWay}" 
                                                            SelectedValuePath="]]><xsl:value-of select="ID"/><![CDATA[" >
                                                    <ComboBox.ItemTemplate>
                                                        <DataTemplate>
                                                            <StackPanel Orientation="Horizontal">
                                                                <TextBlock Text="{Binding ]]><xsl:value-of select="Name"/><![CDATA[}" />
                                                                   <TextBlock Text=" " />
                                                                <TextBlock Text="{Binding Subregnum}" />
                                                         </StackPanel>
                                                        </DataTemplate>
                                                    </ComboBox.ItemTemplate>
                                                </ComboBox>
                                            </StackPanel>      ]]>
</xsl:when>
<xsl:otherwise>     
  <![CDATA[                                           <StackPanel Orientation="Horizontal" >
                                                        <Label Content="{x:Static ]]><xsl:value-of select="Translate"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="Name"/><![CDATA[}" Width="130" HorizontalAlignment="Left" />
                                                        <ComboBox ItemsSource="{Binding ]]><xsl:value-of select="Table"/><![CDATA[AllList}" Width="590" 
                                                                  IsSynchronizedWithCurrentItem="True"  
                                                            SelectedValue="{Binding SelectedItem.]]><xsl:value-of select="ID"/><![CDATA[, ElementName=Tbl90RefSourcesList, Mode=TwoWay}" 
                                                            SelectedValuePath="]]><xsl:value-of select="ID"/><![CDATA[" >
                                                            <ComboBox.ItemTemplate>
                                                                <DataTemplate>
                                                                    <StackPanel Orientation="Horizontal">
                                                                        <TextBlock Text="{Binding ]]><xsl:value-of select="Name"/><![CDATA[}" />
                                                                    </StackPanel>
                                                                </DataTemplate>
                                                            </ComboBox.ItemTemplate>
                                                        </ComboBox>
                                            </StackPanel>  ]]>
</xsl:otherwise>    
</xsl:choose>

<![CDATA[                                                     <StackPanel Orientation="Horizontal" >
                                                        <Label Content="{x:Static tbl90RefSourcesRes:StringsRes.RefSourceName}" Width="130" HorizontalAlignment="Left" />
                                                        <ComboBox ItemsSource="{Binding Tbl90SourcesAllList}" Width="590" 
                                                                  IsSynchronizedWithCurrentItem="True" 
                                                            SelectedValue="{Binding SelectedItem.RefSourceID, ElementName=Tbl90RefSourcesList, Mode=TwoWay}" 
                                                            SelectedValuePath="RefSourceID" 
                                                            DisplayMemberPath="RefSourceName"   />
                                                    </StackPanel>
                                                    <StackPanel Orientation="Horizontal" >
                                                        <Label Content="{x:Static sharedRes:StringsRes.Valid}" Width="130" HorizontalAlignment="Left" />
                                                        <CheckBox IsChecked="{Binding SelectedItem.Valid, ElementName=Tbl90RefSourcesList}" Width="50" HorizontalAlignment="Right"  VerticalAlignment="Center"/>
                                                        <Label Content="{x:Static sharedRes:StringsRes.ValidYear}" Width="130" HorizontalAlignment="Left" />
                                                        <TextBox Text="{Binding SelectedItem.ValidYear, ElementName=Tbl90RefSourcesList}" Width="50" HorizontalAlignment="Right"/>
                                                    </StackPanel>
                                                    <StackPanel Orientation="Horizontal" >
                                                        <Label Content="{x:Static sharedRes:StringsRes.Author}" Width="130" HorizontalAlignment="Left" />
                                                        <TextBox Text="{Binding SelectedItem.Author, ElementName=Tbl90RefSourcesList}" Width="300" HorizontalAlignment="Right"/>
                                                        <Label Content="{x:Static sharedRes:StringsRes.AuthorYear}" Width="130" HorizontalAlignment="Left" />
                                                        <TextBox Text="{Binding SelectedItem.SourceYear, ElementName=Tbl90RefSourcesList}" Width="70" HorizontalAlignment="Right"/>
                                                    </StackPanel>
                                                    <StackPanel Orientation="Horizontal" >
                                                        <Label Content="{x:Static sharedRes:StringsRes.ReportNotes}" Width="130" HorizontalAlignment="Left" />
                                                        <TextBox Text="{Binding SelectedItem.Notes, ElementName=Tbl90RefSourcesList}" Width="590" HorizontalAlignment="Right"  
                                                             TextWrapping="Wrap"  AcceptsReturn="True"  VerticalScrollBarVisibility="Visible" Height="50"/>
                                                    </StackPanel>
                                                    <StackPanel Orientation="Horizontal" >
                                                        <Label Content="{x:Static sharedRes:StringsRes.Info}" Width="130" HorizontalAlignment="Left" />
                                                        <TextBox Text="{Binding SelectedItem.Info, ElementName=Tbl90RefSourcesList}" Width="590" HorizontalAlignment="Right"/>
                                                    </StackPanel>
                                                    <StackPanel Orientation="Horizontal" >
                                                        <Label Content="{x:Static sharedRes:StringsRes.Memo}" Width="130" HorizontalAlignment="Left" />
                                                        <TextBox Text="{Binding SelectedItem.Memo, ElementName=Tbl90RefSourcesList}" Width="590" HorizontalAlignment="Right"  
                                                             TextWrapping="Wrap"  AcceptsReturn="True"  VerticalScrollBarVisibility="Visible" Height="50"/>
                                                    </StackPanel>
                                                </StackPanel>
                                            </HeaderedContentControl>
                                        </TabItem>
                                        <TabItem Header="{x:Static sharedRes:StringsRes.ReferenceAuthor}">
                                            <HeaderedContentControl>
                                                <HeaderedContentControl.Header>
                                                    <StackPanel Orientation="Horizontal" HorizontalAlignment="Right">
                                                        <Button Content="{x:Static sharedRes:StringsRes.ButtonSave}" DockPanel.Dock="Right" Margin="10,2" VerticalAlignment="Center"
                                                            Command="{Binding Path=SaveRefAuthorCommand}" IsDefault="True" Width="100" />
                                                        <TextBlock Margin="0,0,5,0">
                                                            <Hyperlink Command="{Binding Path=AddRefAuthorCommand}"  >
                                                                <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkCreate}"/>
                                                            </Hyperlink>
                                                            | <Hyperlink Command="{Binding Path=DeleteRefAuthorCommand}">
                                                                <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkDelete}"/>
                                                            </Hyperlink>
                                                        </TextBlock>
                                                    </StackPanel>
                                             </HeaderedContentControl.Header>   
                                               <StackPanel>   ]]>
<xsl:choose>
<xsl:when test="Table ='+++++Subregnum++++++++++++++++++'"> 
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">   <![CDATA[   
                                             <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static ]]><xsl:value-of select="Translate"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="Name"/><![CDATA[}" Width="130" HorizontalAlignment="Left" />
                                                   <ComboBox ItemsSource="{Binding ]]><xsl:value-of select="Table"/><![CDATA[AllList}" Width="690" 
                                                                  IsSynchronizedWithCurrentItem="True"  
                                                            SelectedValue="{Binding SelectedItem.]]><xsl:value-of select="ID"/><![CDATA[, ElementName=Tbl90RefAuthorsList, Mode=TwoWay}" 
                                                            SelectedValuePath="]]><xsl:value-of select="ID"/><![CDATA[" >
                                                    <ComboBox.ItemTemplate>
                                                        <DataTemplate>
                                                            <StackPanel Orientation="Horizontal">
                                                                <TextBlock Text="{Binding ]]><xsl:value-of select="Name"/><![CDATA[}" />
                                                                   <TextBlock Text=" " />
                                                                <TextBlock Text="{Binding Subregnum}" />
                                                         </StackPanel>
                                                        </DataTemplate>
                                                    </ComboBox.ItemTemplate>
                                                </ComboBox>
                                            </StackPanel>      ]]>
</xsl:when>
<xsl:otherwise>     
  <![CDATA[                                           <StackPanel Orientation="Horizontal" >
                                                        <Label Content="{x:Static ]]><xsl:value-of select="Translate"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="Name"/><![CDATA[}" Width="130" HorizontalAlignment="Left" />
                                                        <ComboBox ItemsSource="{Binding ]]><xsl:value-of select="Table"/><![CDATA[AllList}" Width="690" 
                                                                  IsSynchronizedWithCurrentItem="True"  
                                                            SelectedValue="{Binding SelectedItem.]]><xsl:value-of select="ID"/><![CDATA[, ElementName=Tbl90RefAuthorsList, Mode=TwoWay}" 
                                                            SelectedValuePath="]]><xsl:value-of select="ID"/><![CDATA[" >
                                                            <ComboBox.ItemTemplate>
                                                                <DataTemplate>
                                                                    <StackPanel Orientation="Horizontal">
                                                                        <TextBlock Text="{Binding ]]><xsl:value-of select="Name"/><![CDATA[}" />
                                                                    </StackPanel>
                                                                </DataTemplate>
                                                            </ComboBox.ItemTemplate>
                                                        </ComboBox>
                                            </StackPanel>  ]]>
</xsl:otherwise>    
</xsl:choose>

<![CDATA[                                                     <StackPanel Orientation="Horizontal" >
                                                        <Label Content="{x:Static sharedRes:StringsRes.Author}" Width="130" HorizontalAlignment="Left" />
                                                        <ComboBox ItemsSource="{Binding Tbl90AuthorsAllList}" Width="690" 
                                                                  IsSynchronizedWithCurrentItem="True" 
                                                            SelectedValue="{Binding SelectedItem.RefAuthorID, ElementName=Tbl90RefAuthorsList, Mode=TwoWay}" 
                                                            SelectedValuePath="RefAuthorID"   >
                                                            <ComboBox.ItemTemplate>
                                                                <DataTemplate>
                                                                    <StackPanel Orientation="Horizontal">
                                                                        <TextBlock Text="{Binding RefAuthorName}" />
                                                                        <TextBlock Text=" / " />
                                                                        <TextBlock Text="{Binding BookName}" />
                                                                        <TextBlock Text=" / " />
                                                                        <TextBlock Text="{Binding Page1}" />
                                                                    </StackPanel>
                                                                </DataTemplate>
                                                            </ComboBox.ItemTemplate>
                                                        </ComboBox>
                                                    </StackPanel>
                                                    <StackPanel Orientation="Horizontal" >
                                                        <Label Content="{x:Static sharedRes:StringsRes.Valid}" Width="130" HorizontalAlignment="Left" />
                                                        <CheckBox IsChecked="{Binding SelectedItem.Valid, ElementName=Tbl90RefAuthorsList}" Width="50" HorizontalAlignment="Right"  VerticalAlignment="Center"/>
                                                        <Label Content="{x:Static sharedRes:StringsRes.ValidYear}" Width="130" HorizontalAlignment="Left" />
                                                        <TextBox Text="{Binding SelectedItem.ValidYear, ElementName=Tbl90RefAuthorsList}" Width="50" HorizontalAlignment="Right"/>
                                                    </StackPanel>
                                                    <StackPanel Orientation="Horizontal" >
                                                        <Label Content="{x:Static sharedRes:StringsRes.Info}" Width="130" HorizontalAlignment="Left" />
                                                        <TextBox Text="{Binding SelectedItem.Info, ElementName=Tbl90RefAuthorsList}" Width="690" HorizontalAlignment="Right"/>
                                                    </StackPanel>
                                                    <StackPanel Orientation="Horizontal" >
                                                        <Label Content="{x:Static sharedRes:StringsRes.Memo}" Width="130" HorizontalAlignment="Left" />
                                                        <TextBox Text="{Binding SelectedItem.Memo, ElementName=Tbl90RefAuthorsList}" Width="690" HorizontalAlignment="Right"  
                                                            Name="TbMultiLine1" TextWrapping="Wrap"  AcceptsReturn="True"  VerticalScrollBarVisibility="Visible" Height="100"/>
                                                    </StackPanel>
                                                </StackPanel>
                                            </HeaderedContentControl>
                                        </TabItem>
                                    </TabControl>
                                </TabItem>
                                <TabItem Header="{x:Static sharedRes:StringsRes.Comment}">
                                    <HeaderedContentControl>
                                        <HeaderedContentControl.Header>
                                            <StackPanel Orientation="Horizontal" HorizontalAlignment="Right">
                                                <Button Content="{x:Static sharedRes:StringsRes.ButtonSave}" DockPanel.Dock="Right" Margin="10,2" VerticalAlignment="Center"
                                                    Command="{Binding Path=SaveCommentCommand}" IsDefault="True" Width="100" />
                                                <TextBlock Margin="0,0,5,0">
                                                <Hyperlink Command="{Binding Path=AddCommentCommand}"  >
                                                    <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkCreate}"/>
                                                </Hyperlink>
                                              | <Hyperlink Command="{Binding Path=DeleteCommentCommand}">
                                                    <TextBlock Text="{x:Static sharedRes:StringsRes.ActionLnkDelete}"/>
                                                </Hyperlink>
                                            </TextBlock>
                                            </StackPanel>
                                        </HeaderedContentControl.Header>                                        
                                               <StackPanel>   ]]>
<xsl:choose>
<xsl:when test="Table ='+++++Subregnum++++++++++++++++++'"> 
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">   <![CDATA[   
                                             <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static ]]><xsl:value-of select="Translate"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="Name"/><![CDATA[}" Width="130" HorizontalAlignment="Left" />
                                                   <ComboBox ItemsSource="{Binding ]]><xsl:value-of select="Table"/><![CDATA[AllList}" Width="520" 
                                                                  IsSynchronizedWithCurrentItem="True"  
                                                            SelectedValue="{Binding SelectedItem.]]><xsl:value-of select="ID"/><![CDATA[, ElementName=Tbl93CommentsList, Mode=TwoWay}" 
                                                            SelectedValuePath="]]><xsl:value-of select="ID"/><![CDATA[" >
                                                    <ComboBox.ItemTemplate>
                                                        <DataTemplate>
                                                            <StackPanel Orientation="Horizontal">
                                                                <TextBlock Text="{Binding ]]><xsl:value-of select="Name"/><![CDATA[}" />
                                                                   <TextBlock Text=" " />
                                                                <TextBlock Text="{Binding Subregnum}" />
                                                         </StackPanel>
                                                        </DataTemplate>
                                                    </ComboBox.ItemTemplate>
                                                </ComboBox>
                                            </StackPanel>      ]]>
</xsl:when>
<xsl:otherwise>     
  <![CDATA[                                           <StackPanel Orientation="Horizontal" >
                                                        <Label Content="{x:Static ]]><xsl:value-of select="Translate"/><![CDATA[Res:StringsRes.]]><xsl:value-of select="Name"/><![CDATA[}" Width="130" HorizontalAlignment="Left" />
                                                        <ComboBox ItemsSource="{Binding ]]><xsl:value-of select="Table"/><![CDATA[AllList}" Width="520" 
                                                                  IsSynchronizedWithCurrentItem="True"  
                                                            SelectedValue="{Binding SelectedItem.]]><xsl:value-of select="ID"/><![CDATA[, ElementName=Tbl93CommentsList, Mode=TwoWay}" 
                                                            SelectedValuePath="]]><xsl:value-of select="ID"/><![CDATA[" >
                                                            <ComboBox.ItemTemplate>
                                                                <DataTemplate>
                                                                    <StackPanel Orientation="Horizontal">
                                                                        <TextBlock Text="{Binding ]]><xsl:value-of select="Name"/><![CDATA[}" />
                                                                    </StackPanel>
                                                                </DataTemplate>
                                                            </ComboBox.ItemTemplate>
                                                        </ComboBox>
                                            </StackPanel>  ]]>
</xsl:otherwise>    
</xsl:choose>

<![CDATA[                                                     <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.Valid}" Width="130" HorizontalAlignment="Left" />
                                                <CheckBox IsChecked="{Binding SelectedItem.Valid, ElementName=Tbl93CommentsList}" Width="50" HorizontalAlignment="Right"  VerticalAlignment="Center"/>
                                                <Label Content="{x:Static sharedRes:StringsRes.ValidYear}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.ValidYear, ElementName=Tbl93CommentsList}" Width="50" HorizontalAlignment="Right"/>
                                            </StackPanel>
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.Info}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.Info, ElementName=Tbl93CommentsList}" Width="520" HorizontalAlignment="Right"/>
                                            </StackPanel>
                                            <StackPanel Orientation="Horizontal" >
                                                <Label Content="{x:Static sharedRes:StringsRes.Memo}" Width="130" HorizontalAlignment="Left" />
                                                <TextBox Text="{Binding SelectedItem.Memo, ElementName=Tbl93CommentsList}" Width="520" HorizontalAlignment="Right"  
                                                             TextWrapping="Wrap"  AcceptsReturn="True"  VerticalScrollBarVisibility="Visible" Height="100"/>
                                            </StackPanel>
                                        </StackPanel>
                                    </HeaderedContentControl>
                                </TabItem>
                            </TabControl>
                        </Border>
                    </StackPanel>
   ]]>         
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='Property HeaderedContentControl  Bottom++++++++++++++++++'"> 
</xsl:when>
<xsl:otherwise>     <![CDATA[   
                </StackPanel>
        </HeaderedContentControl>
   ]]>         
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='Grid Bottom++++++++++++++++++'"> 
</xsl:when>
<xsl:otherwise>     <![CDATA[   
    </Grid>
</UserControl>
   ]]>         
</xsl:otherwise>    
</xsl:choose>

<xsl:choose>
<xsl:when test="Table ='++++++Test Beispiel++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">
   <![CDATA[ 
   ]]>
</xsl:when>  
<xsl:otherwise>
  <xsl:if test="TableFK1 !='NULL'">
       <![CDATA[ 
  ]]> 
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose> 

</xsl:template>
</xsl:stylesheet>











