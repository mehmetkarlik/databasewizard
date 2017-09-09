using System;
using System.Net;
using System.IO;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Reflection;
using System.Data;
using System.Xml;
using System.Collections.Generic;
using System.Web.Services;
namespace DatabaseWizard
{
    public static class ReadWebService
    {

        public static string hata = "";

        public static DataSet WebservicecallWithNoArguments(string adress, string methodName)
        {
            try
            {
                if (!adress.Contains("?wsdl"))
                {
                    adress += "?wsdl";
                }

                WebRequest webRequest = WebRequest.Create(adress);
                WebResponse webResponse = webRequest.GetResponse();
                // Stream stream = webResponse.GetResponseStream(); 
                ServiceDescription description = new ServiceDescription();
                using (Stream stream = webResponse.GetResponseStream())
                {
                    description = ServiceDescription.Read(stream);
                }
                ServiceDescriptionImporter importer = new ServiceDescriptionImporter();


                importer.ProtocolName = "Soap12";//' Use SOAP 1.2. 
                importer.AddServiceDescription(description, null, null);
                importer.Style = ServiceDescriptionImportStyle.Client;

                //'--Generate properties to represent primitive values. 
                importer.CodeGenerationOptions = System.Xml.Serialization.CodeGenerationOptions.GenerateProperties;
                //'Initialize a Code-DOM tree into which we will import the service. 

                CodeNamespace nmspace = new CodeNamespace();
                CodeCompileUnit unit1 = new CodeCompileUnit();
                unit1.Namespaces.Add(nmspace);
                ServiceDescriptionImportWarnings warning = importer.Import(nmspace, unit1);
                CodeDomProvider provider1 = CodeDomProvider.CreateProvider("C#");

                //'--Compile the assembly proxy with the // appropriate references 
                String[] assemblyReferences;
                assemblyReferences = new String[] { "System.dll", "System.Web.Services.dll", "System.Web.dll", "System.Xml.dll", "System.Data.dll" };

                CompilerParameters parms = new CompilerParameters(assemblyReferences);
                parms.GenerateInMemory = true;
                CompilerResults results = provider1.CompileAssemblyFromDom(parms, unit1);
                if (results.Errors.Count > 0)
                {
                }


                Type foundType = null;
                Type[] types = results.CompiledAssembly.GetTypes();
                foreach (Type type1 in types)
                {
                    if (type1.BaseType == typeof(SoapHttpClientProtocol))
                    {
                        foundType = type1;
                    }
                }

                DataSet ds = new DataSet();

                //if (!String.IsNullOrEmpty(contryname))
                //{
                //    Object[] args = new Object[1];
                //    args[0] = contryname;
                //    Object wsvcClass = results.CompiledAssembly.CreateInstance(foundType.ToString());
                //    MethodInfo mi = wsvcClass.GetType().GetMethod(methodName);
                //    var returnValue = mi.Invoke(wsvcClass, args);


                //    ds = ConvertXMLToDataSet(returnValue.ToString());
                //}
                //else
                //{

                //    Object wsvcClass = results.CompiledAssembly.CreateInstance(foundType.ToString());
                //    MethodInfo mi = wsvcClass.GetType().GetMethod(methodName);
                //    var returnValue = mi.Invoke(wsvcClass, null);
                //    ds = ConvertXMLToDataSet(returnValue.ToString());
                //}

                Object wsvcClass = results.CompiledAssembly.CreateInstance(foundType.ToString());
                MethodInfo mi = wsvcClass.GetType().GetMethod(methodName);
                var returnValue = mi.Invoke(wsvcClass, null);
                ds = ConvertXMLToDataSet(returnValue.ToString());


                return ds;
            }
            catch (Exception ex)
            {
                hata = ex.Message;
                return null;
            }



        }

        public static DataSet WebservicecallWithArguments(string adress, string methodName, object[] args)
        {
            try
            {
                if (!adress.Contains("?wsdl"))
                {
                    adress += "?wsdl";
                }

                WebRequest webRequest = WebRequest.Create(adress);
                WebResponse webResponse = webRequest.GetResponse();
                // Stream stream = webResponse.GetResponseStream(); 
                ServiceDescription description = new ServiceDescription();
                using (Stream stream = webResponse.GetResponseStream())
                {
                    description = ServiceDescription.Read(stream);
                }
                ServiceDescriptionImporter importer = new ServiceDescriptionImporter();


                importer.ProtocolName = "Soap12";//' Use SOAP 1.2. 
                importer.AddServiceDescription(description, null, null);
                importer.Style = ServiceDescriptionImportStyle.Client;

                //'--Generate properties to represent primitive values. 
                importer.CodeGenerationOptions = System.Xml.Serialization.CodeGenerationOptions.GenerateProperties;
                //'Initialize a Code-DOM tree into which we will import the service. 

                CodeNamespace nmspace = new CodeNamespace();
                CodeCompileUnit unit1 = new CodeCompileUnit();
                unit1.Namespaces.Add(nmspace);
                ServiceDescriptionImportWarnings warning = importer.Import(nmspace, unit1);
                CodeDomProvider provider1 = CodeDomProvider.CreateProvider("C#");

                //'--Compile the assembly proxy with the // appropriate references 
                String[] assemblyReferences;
                assemblyReferences = new String[] { "System.dll", "System.Web.Services.dll", "System.Web.dll", "System.Xml.dll", "System.Data.dll" };

                CompilerParameters parms = new CompilerParameters(assemblyReferences);
                parms.GenerateInMemory = true;
                CompilerResults results = provider1.CompileAssemblyFromDom(parms, unit1);
                if (results.Errors.Count > 0)
                {
                }


                Type foundType = null;
                Type[] types = results.CompiledAssembly.GetTypes();
                foreach (Type type1 in types)
                {
                    if (type1.BaseType == typeof(SoapHttpClientProtocol))
                    {
                        foundType = type1;
                    }
                }

                DataSet ds = new DataSet();

                //if (!String.IsNullOrEmpty(contryname))
                //{
                //    Object[] args = new Object[1];
                //    args[0] = contryname;
                //    Object wsvcClass = results.CompiledAssembly.CreateInstance(foundType.ToString());
                //    MethodInfo mi = wsvcClass.GetType().GetMethod(methodName);
                //    var returnValue = mi.Invoke(wsvcClass, args);


                //    ds = ConvertXMLToDataSet(returnValue.ToString());
                //}
                //else
                //{

                //    Object wsvcClass = results.CompiledAssembly.CreateInstance(foundType.ToString());
                //    MethodInfo mi = wsvcClass.GetType().GetMethod(methodName);
                //    var returnValue = mi.Invoke(wsvcClass, null);
                //    ds = ConvertXMLToDataSet(returnValue.ToString());
                //}

                Object wsvcClass = results.CompiledAssembly.CreateInstance(foundType.ToString());
                MethodInfo mi = wsvcClass.GetType().GetMethod(methodName);
                var returnValue = mi.Invoke(wsvcClass, args);
                ds = ConvertXMLToDataSet(returnValue.ToString());


                return ds;
            }
            catch (Exception ex)
            {
                hata = ex.Message;
                return null;
            }



        }

        public static string[] GetWebServiceMethods(string adress)
        {
            try
            {
                if (!adress.Contains("?wsdl"))
                {
                    adress += "?wsdl";
                }

                WebRequest webRequest = WebRequest.Create(adress);
                WebResponse webResponse = webRequest.GetResponse();
                // Stream stream = webResponse.GetResponseStream(); 
                ServiceDescription description = new ServiceDescription();
                using (Stream stream = webResponse.GetResponseStream())
                {
                    description = ServiceDescription.Read(stream);
                }
                ServiceDescriptionImporter importer = new ServiceDescriptionImporter();


                importer.ProtocolName = "Soap12";//' Use SOAP 1.2. 
                importer.AddServiceDescription(description, null, null);
                importer.Style = ServiceDescriptionImportStyle.Client;

                //'--Generate properties to represent primitive values. 
                importer.CodeGenerationOptions = System.Xml.Serialization.CodeGenerationOptions.GenerateProperties;
                //'Initialize a Code-DOM tree into which we will import the service. 

                CodeNamespace nmspace = new CodeNamespace();
                CodeCompileUnit unit1 = new CodeCompileUnit();
                unit1.Namespaces.Add(nmspace);
                ServiceDescriptionImportWarnings warning = importer.Import(nmspace, unit1);
                CodeDomProvider provider1 = CodeDomProvider.CreateProvider("C#");

                //'--Compile the assembly proxy with the // appropriate references 
                String[] assemblyReferences;
                assemblyReferences = new String[] { "System.dll", "System.Web.Services.dll", "System.Web.dll", "System.Xml.dll", "System.Data.dll" };

                CompilerParameters parms = new CompilerParameters(assemblyReferences);
                parms.GenerateInMemory = true;
                CompilerResults results = provider1.CompileAssemblyFromDom(parms, unit1);
                if (results.Errors.Count > 0)
                {
                }


                Type foundType = null;
                Type[] types = results.CompiledAssembly.GetTypes();
                foreach (Type type1 in types)
                {
                    if (type1.BaseType == typeof(SoapHttpClientProtocol))
                    {
                        foundType = type1;
                    }
                }

                DataSet ds = new DataSet();

                //if (!String.IsNullOrEmpty(contryname))
                //{
                //    Object[] args = new Object[1];
                //    args[0] = contryname;
                //    Object wsvcClass = results.CompiledAssembly.CreateInstance(foundType.ToString());
                //    MethodInfo mi = wsvcClass.GetType().GetMethod(methodName);
                //    var returnValue = mi.Invoke(wsvcClass, args);


                //    ds = ConvertXMLToDataSet(returnValue.ToString());
                //}
                //else
                //{

                //    Object wsvcClass = results.CompiledAssembly.CreateInstance(foundType.ToString());
                //    MethodInfo mi = wsvcClass.GetType().GetMethod(methodName);
                //    var returnValue = mi.Invoke(wsvcClass, null);
                //    ds = ConvertXMLToDataSet(returnValue.ToString());
                //}

                Object wsvcClass = results.CompiledAssembly.CreateInstance(foundType.ToString());
                MethodInfo[] mi = wsvcClass.GetType().GetMethods();


                List<string> methods = new List<string>();

                for (int i = 0; i < mi.Length; i++)
                {
                    if (mi[i].GetCustomAttributesData().Count > 0)
                    {
                        MethodInfo m = mi[i];
                        object[] o = m.GetCustomAttributes(true);

                        try
                        {
                            SoapDocumentMethodAttribute att = (SoapDocumentMethodAttribute)o[0];
                            methods.Add(mi[i].Name);
                        }
                        catch (Exception)
                        {

                        }


                    }

                }

                return methods.ToArray();
            }
            catch (Exception ex)
            {
                hata = ex.Message;
                return null;
            }



        }

        public static ParameterInfo[] GetWebServiceMethodsParameters(string adress, string methodName)
        {
            try
            {
                if (!adress.Contains("?wsdl"))
                {
                    adress += "?wsdl";
                }

                WebRequest webRequest = WebRequest.Create(adress);
                WebResponse webResponse = webRequest.GetResponse();
                // Stream stream = webResponse.GetResponseStream(); 
                ServiceDescription description = new ServiceDescription();
                using (Stream stream = webResponse.GetResponseStream())
                {
                    description = ServiceDescription.Read(stream);
                }
                ServiceDescriptionImporter importer = new ServiceDescriptionImporter();


                importer.ProtocolName = "Soap12";//' Use SOAP 1.2. 
                importer.AddServiceDescription(description, null, null);
                importer.Style = ServiceDescriptionImportStyle.Client;

                //'--Generate properties to represent primitive values. 
                importer.CodeGenerationOptions = System.Xml.Serialization.CodeGenerationOptions.GenerateProperties;
                //'Initialize a Code-DOM tree into which we will import the service. 

                CodeNamespace nmspace = new CodeNamespace();
                CodeCompileUnit unit1 = new CodeCompileUnit();
                unit1.Namespaces.Add(nmspace);
                ServiceDescriptionImportWarnings warning = importer.Import(nmspace, unit1);
                CodeDomProvider provider1 = CodeDomProvider.CreateProvider("C#");

                //'--Compile the assembly proxy with the // appropriate references 
                String[] assemblyReferences;
                assemblyReferences = new String[] { "System.dll", "System.Web.Services.dll", "System.Web.dll", "System.Xml.dll", "System.Data.dll" };

                CompilerParameters parms = new CompilerParameters(assemblyReferences);
                parms.GenerateInMemory = true;
                CompilerResults results = provider1.CompileAssemblyFromDom(parms, unit1);
                if (results.Errors.Count > 0)
                {
                }


                Type foundType = null;
                Type[] types = results.CompiledAssembly.GetTypes();
                foreach (Type type1 in types)
                {
                    if (type1.BaseType == typeof(SoapHttpClientProtocol))
                    {
                        foundType = type1;
                    }
                }

                DataSet ds = new DataSet();

                //if (!String.IsNullOrEmpty(contryname))
                //{
                //    Object[] args = new Object[1];
                //    args[0] = contryname;
                //    Object wsvcClass = results.CompiledAssembly.CreateInstance(foundType.ToString());
                //    MethodInfo mi = wsvcClass.GetType().GetMethod(methodName);
                //    var returnValue = mi.Invoke(wsvcClass, args);


                //    ds = ConvertXMLToDataSet(returnValue.ToString());
                //}
                //else
                //{

                //    Object wsvcClass = results.CompiledAssembly.CreateInstance(foundType.ToString());
                //    MethodInfo mi = wsvcClass.GetType().GetMethod(methodName);
                //    var returnValue = mi.Invoke(wsvcClass, null);
                //    ds = ConvertXMLToDataSet(returnValue.ToString());
                //}

                Object wsvcClass = results.CompiledAssembly.CreateInstance(foundType.ToString());
                MethodInfo mi = wsvcClass.GetType().GetMethod(methodName);

                ParameterInfo[] parameters = mi.GetParameters();

                

                return parameters;
            }
            catch (Exception ex)
            {
                hata = ex.Message;
                return null;
            }



        }

        private static DataSet ConvertXMLToDataSet(string xmlData)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmlData);
                // Load the XmlTextReader from the stream 
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                return xmlDS;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }
        //protected void grdcountry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    Webservicecall("");
        //    grdcountry.PageIndex = e.NewPageIndex;
        //    grdcountry.DataBind();
        //}
        //protected void grdcountry_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName.Equals("Loaddata"))
        //    {
        //        Webservicecall(e.CommandArgument.ToString());
        //    }
        //}
    }
}
