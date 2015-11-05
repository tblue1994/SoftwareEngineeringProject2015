module RandomForestModel

open Charon
open System
open FSharp.Data

(*
We use the CSV type provider from FSharp.Data 
to extract passenger information,
renaming properties for convenience:
- Pclass: Class, 
- Parch: ParentsOrChildren, 
- SibSp: SiblingsOrSpouse
We force inference to treat every feature as optional, 
assuming that any feature could have missing values.
*)

type DataSet = CsvProvider<"""./data-small.csv""",
                           Schema="RID=int,HEIGHT(m)->Height=float,WEIGHT(kg)->Weight=float,DURATION(sec)->Duration=float,DISTANCE(m)->Distance=float,STEPS->Steps=int,TYPE(R|J|W|B|O)->Type">

type Passenger = DataSet.Row

let getData() = 
    use data = DataSet.Load("https://dl.dropboxusercontent.com/u/100487576/data-small.csv")
    [| for passenger in Seq.take 5000 data.Rows -> passenger, passenger |]

let getLabels() = "Type", (fun (obs:Passenger) -> Some obs.Type) |> Categorical

let getFeatures() = 
    [ 
        "Weight", (fun (o:Passenger) -> Some o.Weight) |> Numerical;
        "Height", (fun (o:Passenger) -> Some o.Height) |> Numerical;
        "Duration", (fun (o:Passenger) -> Some o.Duration) |> Numerical;
        "Distance", (fun (o:Passenger) -> Some o.Distance) |> Numerical;
        "Steps", (fun (o:Passenger) -> Some o.Steps) |> Numerical;
    ]

let train training labels features = basicTree training (labels, features) DefaultSettings

let trainTree() =
    // We read the training set into an array,
    // defining the Label we want to classify on:
    let training = getData()

    // We define the label, and what features should be used:
    let labels = getLabels()
    
    let features = getFeatures()

    forest training (labels,features) DefaultSettings

let applyTree (tree:ForestResults<obj, DataSet.Row>) (x: DataSet.Row) =
    tree.Classifier x

// Wrap a row in a tuple for the c# code.
let wrapRow (a: int, b: string, c: string, d: DateTime, e: double, f: double, g: double, h: double, i: int, j: string) =
    (a, b, c, d, e, f, g, h, i, j)