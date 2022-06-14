console.log("tes latihan");

var a = 1;
console.log(a);

let array = [1, 2, 3, 4, "push"];
console.log(array);

array.unshift("tes");
console.log(array);

array.shift();
console.log(array);

let arrayMulti = ['a', 'b', 'c', [1, 2, 3, ['hallo']], true];
console.log(arrayMulti[3][3][0])

let mhs = {
    nama: "yuda",
    nim: "12332",
    jurusan: "Informatika",
    umur: 23,
    hobby: ["mancing", "tidur", "rebahan"],
    isActive: true
}


const nilai = 70;
console.log(nilai);

console.log(mhs.hobby[2])

let user = {};
user.username = "yuda";
user.password = "yuda";
console.log(user);

user.username = "xxx";
key = "password";
console.log(user[key]);

const csv = "1|2|3";
const [one, two, three] = csv.split("1");
console.log(three)

//array of object
const animals = [
    { name: "Garfield", species: "cat", class: { name: "mamalia" } },
    { name: "Nemo", species: "fish", class: { name: "invertebrata" } },
    { name: "Tom", species: "cat", class: { name: "mamalia" } },
    { name: "Bruno", species: "fish", class: { name: "invertebrata" } },
    { name: "Carlo", species: "cat", class: { name: "mamalia" } },
]
console.log(animals);

let onlyCats = []
animals.forEach(function (animal) {

    if (animal.species === "cat") {
        onlyCats.push(animal);
    } else {
        animal.class.name = "non mamalia";
    }
})

console.log(onlyCats);
console.log(animals);



/*const OnlyCat = [];

for (var i = 0; i < animals.length; i++) {
    if (animals[i].species == "fish") {
        animals[i].class.name = "non mamalia";
    }
    else if (animals[i].species == "cat") {
        OnlyCat.push(animals[i]);
    }
}

console.log(animals);

console.log(OnlyCat);*/



/*// loop
for (var i = 0; i < animals.length; i++) {
    console.log("hasil loop : ",animals[i]);
}*/



/*//Find index of specific object using findIndex method.    
let objIndex = animals.findIndex((obj => obj.species == "fish"));

// find and loop
for (var a = 0; a < objIndex.length; a++) {
    //console.log("hasil loop : ", animals[a]);
//Log object to Console.
    console.log("Before update: ", animals[a]);
}

//Update object's name property.
animals[objIndex].class.name = "non mamalia"

//Log object to console again.
console.log("After update: ", animals[objIndex])
*/