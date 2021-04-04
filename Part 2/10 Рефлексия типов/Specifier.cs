using System.Linq;
using System.Reflection;

namespace Documentation
{
    public class Specifier<T> : ISpecifier
    {
        public string GetApiDescription()
        {
            var result = typeof(T).GetCustomAttributes().OfType<ApiDescriptionAttribute>().FirstOrDefault();
            return result?.Description;
        }
        public string[] GetApiMethodNames()
        {
            return typeof(VkApi).GetMethods().Where(x => (x.CustomAttributes.Select(e => e.AttributeType.Name)).Contains("ApiMethodAttribute"))
                                             .Select(x => x.Name).ToArray();
        }
        public string GetApiMethodDescription(string methodName)
        {
            var methodInfo = GetMethodInfo(methodName);
            return methodInfo?.GetCustomAttribute<ApiDescriptionAttribute>().Description;
        }
        public string[] GetApiMethodParamNames(string methodName)
        {
            var methodInfo = GetMethodInfo(methodName);
            return methodInfo?.GetParameters().Select(x => x.Name).ToArray();
        }

        public string GetApiMethodParamDescription(string methodName, string paramName)
        {
            var methodInfo = GetMethodInfo(methodName);
            var parametrInfo = GetParameterInfo(methodInfo, paramName);
            return parametrInfo?.GetCustomAttribute<ApiDescriptionAttribute>()?.Description;
        }
        public ApiParamDescription GetApiMethodParamFullDescription(string methodName, string paramName)
        {
            var methodInfo = GetMethodInfo(methodName);
            var parametrInfo = GetParameterInfo(methodInfo, paramName);
            var attribute = parametrInfo?.GetCustomAttributes();
            return new ApiParamDescription()
            {
                Required = (attribute?.OfType<ApiRequiredAttribute>().FirstOrDefault()) != null && attribute.OfType<ApiRequiredAttribute>()
                                                                                                                .FirstOrDefault().Required,
                ParamDescription = new CommonDescription(paramName, attribute?.OfType<ApiDescriptionAttribute>().FirstOrDefault()?.Description),
                MaxValue = attribute?.OfType<ApiIntValidationAttribute>().FirstOrDefault()?.MaxValue,
                MinValue = attribute?.OfType<ApiIntValidationAttribute>().FirstOrDefault()?.MinValue
            };
        }
        public ApiMethodDescription GetApiMethodFullDescription(string methodName)
        {
            var methodInfo = GetMethodInfo(methodName);
            if (methodInfo == null) return null;
            return methodInfo == null ? null : new ApiMethodDescription()
            {
                MethodDescription = new CommonDescription(methodName, GetApiMethodDescription(methodName)),
                ParamDescriptions = GetApiMethodParamNames(methodName).Select(x => GetApiMethodParamFullDescription(methodName, x)).ToArray(),
                ReturnDescription = GetApiParamDescription(methodInfo)
            };
            ;
        }
        private ApiParamDescription GetApiParamDescription(MethodInfo methodInfo)
        {
            var attributes = methodInfo.ReturnTypeCustomAttributes.GetCustomAttributes(false);
            return attributes.Length == 0 ? null : new ApiParamDescription()
            {
                Required = (bool)(attributes?.OfType<ApiRequiredAttribute>().FirstOrDefault() == null ?
                                            false : attributes?.OfType<ApiRequiredAttribute>().FirstOrDefault().Required),
                MaxValue = attributes?.OfType<ApiIntValidationAttribute>().FirstOrDefault()?.MaxValue,
                MinValue = attributes?.OfType<ApiIntValidationAttribute>().FirstOrDefault()?.MinValue,
                ParamDescription = new CommonDescription()
            };
            ;
        }
        private MethodInfo GetMethodInfo(string methodName)
        {
            return typeof(T).GetMethods().Where(x => (x.CustomAttributes.Select(e => e.AttributeType.Name)).Contains("ApiMethodAttribute"))
                                         .Where(x => x.Name == methodName).FirstOrDefault();

        }
        private ParameterInfo GetParameterInfo(MethodInfo methodInfo, string parametrName)
        {
            return methodInfo?.GetParameters().Where(x => x.Name == parametrName).FirstOrDefault();
        }
    }
}