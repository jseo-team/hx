using System;
using System.IO; using System.Collections; 
using System.Collections.Generic; using System.Linq; 
using System.Dynamic; using System.Reflection;

namespace CSEO{

public class Wise : DynamicObject
{
    
  Dictionary<string, dynamic> dictionary = new Dictionary<string, dynamic>();
  List<string> list = new List<string>();


    private load reloading;
    private indexa indexing;
    private filter filtering;

    public Wise()
    {
        reloading = defaultLoader;
        indexing = defaultIndexer;
        filtering = defaultFilter;
    }

    public Wise(string[] stack){
        reloading = defaultLoader;
        indexing = defaultIndexer;
        filtering = defaultFilter;

        list.AddRange(stack);
    }

    public Wise(object stack){

        reloading = defaultLoader;
        indexing = defaultIndexer;
        filtering = defaultFilter;

            Type type = stack.GetType();
            FieldInfo[] field = type.GetFields();
            PropertyInfo[] myPropertyInfo = type.GetProperties();

            String value = null;

        foreach (var propertyInfo in myPropertyInfo)
        {
            if (propertyInfo.GetIndexParameters().Length == 0)
            {
                value = propertyInfo.GetValue(stack, null) as String;
                dictionary.Add(propertyInfo.Name.ToString(), value);
            }
        }
    }

    public bool hasOwnProperty(string property)
    {
        return dictionary.ContainsKey(property);
    }

    public void setLoading(string code){
        //todo: compile
        reloading = defaultLoader;
    }

    public void setIndexing(string code){
        //todo: compile
        filtering = defaultFilter;
    }

    private string defaultLoader(string added) {
        return added;
    }

    private void defaultFilter(string those, string stack, ref String result) 
    {
            string filterName = result + "";

        if (filterName!="") filterName += ",";

        filterName += those;

        result = filterName;
    }

    private void defaultIndexer(string name, string code) 
    {
        load newFilter = (stack) => {
                
                string result = "";

            foreach(string those in list){ 
                filtering(those, stack, ref result); }

            return result;

        };

        dictionary[name] = newFilter;
    }


    public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
        return dictionary.TryGetValue(binder.Name.ToLower(), out result);
    }

    public override bool TrySetMember(SetMemberBinder binder, object value)
    {
        dictionary[binder.Name.ToLower()] = value;return true;
    }

    public override bool TryGetIndex (System.Dynamic.GetIndexBinder binder, object[] indexes, out object result)
    {
            int index = (int)indexes[0];

        try
        {
            result = list[index];
            return true;

        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }

        result = null;
        return false;
    }

    public override bool TrySetIndex (System.Dynamic.SetIndexBinder binder, object[] indexes, object value)
    {
            int index = (int)indexes[0];

        try
        {
            list[index] = "" +(value);
            return true;

        }catch(Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }

    public int index{
        get; set;
    }

    public void add(string added){
        list.Add(reloading(added));
    }

    public string first
    {
        get
        {
            index = 0;
            return list[index];
        }

        set
        {   
            index = 0;
            list[index] = value;
        }
    }

    public string next
    {
        get
        {
            index++;
            if (index==list.Count)
                index=list.Count-1;
            return list[index];
        }

        set
        {   
            index++;
            if (index==list.Count)
                index=list.Count-1;
            list[index] = value;
        }

    }

    public string last
    {
        get
        {
            index = list.Count-1;
            return list[index];
        }

        set
        {   
            index = list.Count-1;
            list[index] = value;
        }
    }

    public string previous
    {
        get
        {
            if (index<=0) index=1;
            index--;
            return list[index];
        }

        set
        {   
            if (index<=0) index=1;
            index--;
            list[index] = value;
        }

    }

    public Wise skipCurrent
    {
        get
        {
            return new Wise(list.Skip(index+1).ToArray());
        }
    }

    public Wise takeCurrent
    {
        get
        {
            return new Wise(list.Take(index+1).ToArray());
        }
    }

    // TODO : PLUS - MINUS
    public int pus=-1;
    public int mus=1;

    public void plus(){

    }

    public void minus(){

    }
    // TODO



    public override string ToString()
    {
        return String.Join(",",list.ToArray());
    }

    //TODO
    public string getter()
    {
        return "";
    }

    //TODO
    public string setter(string stack)
    {
        return "";
    }

    public string stack
    {
        get{
            return getter();
        }
        set{
            setter(value);
        }
    }

    public void indexer(string name, string code)
    {
        indexing(name, code);
    }

    public void loader(string name, string code)
    {
    
            Wise newLoader = dictionary[name] as Wise;
        if (newLoader==null) newLoader = new Wise();

        newLoader.setLoading(code);
        dictionary[name] = newLoader;
    }

    public void module(string name, string code)
    {
        
            Wise newModule = dictionary[name] as Wise;
        if (newModule==null) newModule = new Wise();

        newModule.setIndexing(code);
        dictionary[name] = newModule;
    }

    //TODO
    public string WON(string script)
    {
      return "cs.add(\""+ "Hey" +"\");"
        + "cs.add(\""+ "I am a collection" +"\");";
    }

    //TODO
    public void JSON(string script)
    {

    }

    //TODO
    public void XML(string script)
    {

    }

}

}
