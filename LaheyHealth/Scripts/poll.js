//Class that keeps information stored for the answers 
//(pushed into answers array after populated if the answer is not already 
//in there and is not the same)
var answers = [];
$(document).ready(function () {
    $('.answer').unbind("click");
    //Array that will store answers to be sent via ajax to post method in item controller
    $('#next').unbind("click");

    //Get selected answer
    $('.answer').on("click", function () {
        var selected = $(this);
        //console.log(selected.text());
        var input = selected.find("input:radio");
        var valueTypeLabel = input.attr("data-valueType"); //Label for value type
        var itemId = input.attr("name"); //Id for item being answered
        var valueTypeId = input.val(); //Id for value type selected as answer
        //Check type of answer that is being inserted
        if (valueTypeLabel == "Skill") {
            //Perform insertion of information for skill value input
            //Check if the item exists in the array of answers
            var AnswerAux = getItemFromArray(itemId, answers);
            console.log(answers);
            if (AnswerAux != null){
                //Update answer 
                AnswerAux.IdSelectedSkill = valueTypeId;
                //Check if value has been inserted for Importance
                if(AnswerAux.IdSelectedImportance != null){
                    //If the value is not null, this means that all values have been assigned
                    //Check AllAssigned as true
                    AnswerAux.AllAssigned = true;
                }
            }
            else
                //Insert new item
                create_insert_item("Skill",itemId,valueTypeId,answers)
        }
        if (valueTypeLabel == "Importance") {
            //Perform insertion of information for importance value input
            //Check if the item exists on the array of answers
            var AnswerAux = getItemFromArray(itemId,answers);
            if (AnswerAux  != null){
                //Update answer
                AnswerAux.IdSelectedImportance = valueTypeId;
                if (AnswerAux.IdSelectedSkill != null){
                    //If the value is not null, this means that all values have been assigned
                    //Check AllAssigned as true
                    AnswerAux.AllAssigned = true;
                }
            }

            else
                //Insert new item
                create_insert_item("Importance", itemId, valueTypeId,answers)
        }

    })

    $("#next").on("click", function (event) {
        event.preventDefault();
        $(this).attr("disabled", true);
        insertAnswers();
    });

    //Click on finish poll
    $("#finish").unbind("click");
    $("#finish").on("click", function (event) {
        event.preventDefault();
        $(this).attr("disabled", true);
        insertAnswers();
    });

});

//Returns item if it is inside of answers array
function getItemFromArray(itemId,anwers) {
    var itemExists = false;
    var item = null;
    var i = 0;
    while (!itemExists && i < answers.length) {
        if (answers[i].Id == itemId) {
            itemExists = true;
            item = answers[i];
        }
        i++;
    }
    return item;
}


//Creates new item and inserts the value inserted for the specific type
//Items only come in here if they don't exist in the array of answers
function create_insert_item(typeValue, itemId, create_insert_item, answers) {
    //Creating new item to be inserted in answers
    var AnswerAux = {};
    AnswerAux.Id = itemId;  //Id of item object that is being answered
    if(typeValue == "Skill"){
        AnswerAux.IdSelectedSkill = create_insert_item;  // Id of skill value selected
        //Set value for Importance to null
        AnswerAux.IdSelectedImportance = null; //Set to null so it can be updated and checked against to inform if all values are in
    }
    if(typeValue == "Importance"){
        AnswerAux.IdSelectedImportance = create_insert_item; // Id of Importance value selected
        AnswerAux.IdSelectedSkill = null; //Set to null so it can be updated and checked against to inform if all values are in
    }
    AnswerAux.AllAssigned = false; //This value gets updated once all value types are in the object

    //Push new item with answers onto array
    answers.push(AnswerAux);
}

//Ajax method that sends answers in for user
function insertAnswers() {
    console.log(answers);
    lstAnswers = JSON.stringify({ answers });
    console.log(lstAnswers);
    $.ajax({
        contentType: "application/json; charset=utf-8",
        type: "POST",
        url: "Poll",
        datatype: "json",
        traditional: true,
        data: lstAnswers,
        success: function (data) {
            //If all answers inserted but not finished send to next set of questions
            if (data.message == "Change to new set of questions") {
                //Redirect to new set of questions
                //onsole.log("Url to redirect: " + data.url);
                //window.location.href = data.url;
                location.reload();
            }
            //If all answers inserted but finishend send to results page
            if (data.message == "Poll Finished") {
                location.href = 'Results';
            }
            if (data.error) {
                $("#next").attr("disabled", false);
                $("#finish").attr("disabled", false);
                $('.alert').fadeIn();
                $('.alert').html("<p>" + data.message + "</p>");
            }

        },
        error: function (error) {
            console.log("Error:" + error);
        }
    });
    

}
