using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

using Mono;
using Mono.Cecil;
using Mono.Cecil.Cil;


namespace SRXD.SpinSearch.Patcher;

public static class Patcher
{
    public static IEnumerable<string> TargetDLLs { get; } = new[] { "Assembly-CSharp.dll" };

    private static Dictionary<string, TypeDefinition> typeLookup = new();
    private static ModuleDefinition mainModule;
    private static FieldDefinition uselessFieldDefinition;

    public static void Patch(AssemblyDefinition assembly)
    {
        mainModule = assembly.MainModule;
        uselessFieldDefinition = new("__uselessField",
            FieldAttributes.Public, mainModule.ImportReference(typeof(bool)));

        CreateDummyMethod("XDLevelSelectMenuBase", "OnGUI");
        CreateDummyMethod("XDLevelSelectMenuBase", "OnEnable");
    }

    private static void CreateDummyMethod(string typeName, string methodName)
    {
        TypeDefinition baseType;
        if (!typeLookup.TryGetValue(typeName, out baseType))
        {
            baseType = mainModule.Types.First(t => t.Name == typeName);
            typeLookup[typeName] = baseType;
            baseType.Fields.Add(uselessFieldDefinition);
        }

        MethodDefinition method = new(methodName, MethodAttributes.Public,
            mainModule.TypeSystem.Void);
        baseType.Methods.Add(method);

        // We have to emit code so that the method does not get inlined
        // Applying a 'not' to a useless field should do the trick
        ILProcessor il = method.Body.GetILProcessor();
        il.Emit(OpCodes.Ldarg_0);
        il.Emit(OpCodes.Ldarg_0);
        il.Emit(OpCodes.Ldfld, uselessFieldDefinition);
        il.Emit(OpCodes.Ldc_I4_0);
        il.Emit(OpCodes.Ceq);
        il.Emit(OpCodes.Stfld, uselessFieldDefinition);
        il.Emit(OpCodes.Ret);

        Trace.TraceInformation($"Created dummy method for {baseType.FullName}+{methodName}");
    }
}
