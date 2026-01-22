function ConnError()
{
alert("Null Connection String!");
close();
}

function QueryError()
{
alert("Null Query String!");
close();
}

function ReportError()
{
alert("Invalid Report Name or Path!");
close();
}

function ReportError2(strpath, strparam, strlevel)
{
alert("Invalid Report Name or Path  "+strpath+" .. "+strparam+" .. "+strlevel);
close();
}

function ParamError()
{
alert("Invalid Parameter Name or Value!");
close();
}

function DBError()
{
alert("Invalid User ID or Password or Data Source or Database");
close();
}
