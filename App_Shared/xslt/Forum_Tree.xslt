<?xml version="1.0" encoding="UTF-8" ?>
<x:stylesheet version="1.0" xmlns:x="http://www.w3.org/1999/XSL/Transform">
	<x:output method="html" omit-xml-declaration="yes" indent="no" />
	<x:param name="IDx" />
	<x:template match="/">
		<x:if test="count(/root/item)!=0">
			<x:for-each select="/root/item">
				<x:call-template name="item">
				    <x:with-param name="level" select="0"/>
		        </x:call-template>
			</x:for-each>
		</x:if>
	</x:template>
	<x:template name="item">
        <x:param name="level" />
        <x:param name="levelx" />
		<div id="{@uid}" class="box2 forum-level{$level}">
			<h3><x:value-of select="@subject" /></h3>
			<p class="title" style="font-size:80%"><a class="pamatovatklik" style="float:right; margin-right:3px;" href="/Forum_Add.aspx?id={$IDx}&amp;reply={@uid}">» Odpovědět «</a><x:value-of select="from/@name" />, <x:value-of select="concat(substring(@date, 7, 2), '.', substring(@date, 5, 2), '.', substring(@date, 1, 4), ' ', substring(@date, 9, 2), ':', substring(@date, 11, 2))" /></p>
			<p><x:value-of select="text" disable-output-escaping="yes" /></p>
		</div>
		<x:if test="count(item)!=0">
			<div style="padding-left:10px;">
				<x:for-each select="item">
					<x:call-template name="item">
						<x:with-param name="level" select="number($level)+1" />
					</x:call-template>
				</x:for-each>
			</div>
		</x:if>
	</x:template>
</x:stylesheet>