
/*tampilan text
$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon/",
}).done((result) => {
    console.log(result.results);
    let text = "";
    $.each(result.results, function (key, val) {
        //console.log(val.name);
        text += `<li>${val.name}</li>`;
    })
    console.log(text);
    $('#ListPoke').html(text);
})*/


$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon/",
}).done((result) => {
    console.log(result.results);
    let text = "";
    $.each(result.results, function (key, val) {
        //console.log(val.name);
        text += `<tr>
                <td>${key+1}</td>
                <td>${val.name}</td>
                <td>${val.url}</td>
                <td>
                    <button type="button" onClick="detailPoke('https://pokeapi.co/api/v2/pokemon/${key + 1}')" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal">
                    <i class="far fa-eye"></i>
                    </button>
                </td>
                </tr>`;
    })
    console.log(text);
    $('#tablePoke').html(text);
})

/*function detailPoke(urlPoke) {
    console.log(urlPoke);
    $('#urlPoke').html(urlPoke);
}*/

function detailPoke(urlPoke) {
    $.ajax({
        url: urlPoke
    }).done(u => {
        $("#namePoke").text(u.name);
        $("#imgPoke").attr("src", u.sprites.other.dream_world.front_default);
        $("#valWeight").text(u.weight);
        $("#valHeight").text(u.height);
        $("#hpStat").width(u.stats[0].base_stat + "%").text(" HP: "+u.stats[0].base_stat + "%");
        $("#attStat").width(u.stats[1].base_stat + "%").text("Attack: "+u.stats[1].base_stat + "%");
        $("#defStat").width(u.stats[2].base_stat + "%").text("Defense: "+u.stats[2].base_stat + "%");
        $("#specAttStat").width(u.stats[3].base_stat + "%").text("Special-Attack: "+u.stats[3].base_stat + "%");
        $("#specDefStat").width(u.stats[4].base_stat + "%").text("Special-Defense: "+u.stats[4].base_stat + "%");
        $("#speedStat").width(u.stats[5].base_stat + "%").text("Speed: " + u.stats[5].base_stat + "%");
        $("#grassType").text(u.types[0].type.name);
        $("#poisonType").text(u.types[1].type.name);
        $("#ability1").text(u.abilities[0].ability.name);
        //$("#ability1det").text(u.abilities[0].ability.effect_entries[0].effect);
        $("#ability2").text(u.abilities[1].ability.name);
        $("#species").text(u.species.name);
        $("#mv0").text(u.moves[0].move.name);
        $("#mv1").text(u.moves[1].move.name);
        $("#mv2").text(u.moves[2].move.name);
        $("#mv3").text(u.moves[3].move.name);
        $("#mv4").text(u.moves[4].move.name);
        $("#mv5").text(u.moves[5].move.name);
        $("#mv6").text(u.moves[6].move.name);
        $("#mv7").text(u.moves[7].move.name);
    })
}


/*function capitalizeFirstLetter(string) {
    return string.charAt(0).toUpperCase() + string.slice(1);
}*/
