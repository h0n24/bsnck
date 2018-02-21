Public Class Val

	Public Shared Function ToDecimal(ByVal value As Object) As Decimal
		Try
			Dim DecimalSeperator As String = Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
			If value.ToString.IndexOf(".") <> -1 Then value = value.ToString.Replace(".", DecimalSeperator)
			If value.ToString.IndexOf(",") <> -1 Then value = value.ToString.Replace(",", DecimalSeperator)
			Return System.Convert.ToDecimal(value)
		Catch ex As Exception
			Return 0
		End Try
	End Function
	Public Shared Function ToInt(ByVal value As Object) As Int64
		Try
			Return System.Convert.ToInt64(value)
		Catch ex As Exception
			Return 0
		End Try
	End Function
	Public Shared Function ToInt16(ByVal value As Object) As Int16
		Try
			Return System.Convert.ToUInt16(value)
		Catch ex As Exception
			Return 0
		End Try
	End Function
	Public Shared Function ToInt32(ByVal value As Object) As Int32
		Try
			Return System.Convert.ToInt32(value)
		Catch ex As Exception
			Return 0
		End Try
	End Function
	Public Shared Function ToInt64(ByVal value As Object) As Int64
		Try
			Return System.Convert.ToInt64(value)
		Catch ex As Exception
			Return 0
		End Try
	End Function

	Public Shared Function ToBoolean(ByVal value As Object) As Boolean
		Try
			Return System.Convert.ToBoolean(value)
		Catch ex As Exception
			Return False
		End Try
	End Function

	Public Shared Function Hex2Int(ByVal HexNum As String) As Long
		Dim Cislo As Long
		Dim f As Integer
		Dim Ascii As Integer
		HexNum = HexNum.ToUpper
		If HexNum.Length > 0 Then
			For f = 0 To HexNum.Length - 1
				Ascii = Asc(HexNum.Substring(f, 1))
				If Ascii >= 48 And Ascii <= 57 Then Cislo = Cislo * 16 + Ascii - 48
				If Ascii >= 65 And Ascii <= 70 Then Cislo = Cislo * 16 + Ascii - 55
			Next
		End If
		Return Cislo
	End Function

	Public Shared Function DateForDatatableSelect(ByVal Datum As Date) As String
		Return "#" & Datum.Month & "/" & Datum.Day & "/" & Datum.Year & " " & Datum.Hour & ":" & Datum.Minute & ":" & Datum.Second & "#"
	End Function

	Public Shared Function ToDate(ByVal DateString As String) As DateTime
		Try
			Return System.Convert.ToDateTime(DateString)
		Catch ex As Exception
			Return Nothing
		End Try
	End Function

	Public Class Types

		Public Shared Function [Get](ByRef Value As Object) As ValueTypes
			If Value Is Nothing Then
				Return ValueTypes.Nothing
			Else
				Return GetFromType(Value.GetType)
			End If
		End Function

		Public Shared Function GetFromType(ByRef Type As System.Type) As ValueTypes
			Dim C As System.TypeCode = System.Type.GetTypeCode(Type)
			Select Case C
				Case TypeCode.Boolean
					Return ValueTypes.Boolean
				Case TypeCode.DateTime
					Return ValueTypes.Date
				Case TypeCode.Object
					Return ValueTypes.Object
				Case TypeCode.DBNull, TypeCode.Empty
					Return ValueTypes.Nothing
				Case TypeCode.String, TypeCode.Char
					Return ValueTypes.Text
				Case TypeCode.Byte, TypeCode.Decimal, TypeCode.Double, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64, TypeCode.SByte, TypeCode.Single, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64
					Return ValueTypes.Number
			End Select
		End Function

		Public Enum ValueTypes As Byte
			Number = 1
			Text = 2
			[Date] = 3
			[Boolean] = 4
			[Object] = 5
			[Nothing] = 6
		End Enum

	End Class

End Class