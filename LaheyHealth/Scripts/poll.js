var answers = [];
$(document).ready(function () {
    $('.answer').unbind("click");
    //Array that will store answers to be sent via ajax to post method in item controller
    
    //Class that keeps information stored for the answers 
    //(pushed into answers array after populated if the answer is not already 
    //in there and is not the same)
    var item = {};
    item.id = 0;  //Id of item object that is being answered
    item.IdSelectedSkill = 0;  // Id of skill value selected
    item.IdSelectedImportance = 0; // Id of Importance value selected

    
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
            var item = getItemFromArray(itemId,answers);
            if (item != null)
                //Update answer 
                item.IdSelectedSkill = valueTypeId;
            else
                //Insert new item
                create_insert_item("Skill",itemId,valueTypeId,answers)
        }
        if (valueTypeLabel == "Importance") {
            //Perform insertion of information for importance value input
            //Check if the item exists on the array of answers
            var item = getItemFromArray(itemId,answers);
            if (item  != null)
                //Update answer
                item.IdSelectedImportance = valueTypeId;
            else
                //Insert new item
                create_insert_item("Importance", itemId, valueTypeId,answers)
        }

        console.log(answers);
    })
});

//Returns item if it is inside of answers array
function getItemFromArray(itemId,anwers) {
    var itemExists = false;
    var item = null;
    var i = 0;
    while (!itemExists && i < answers.length) {
        if (answers[i].id == itemId) {
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
    var item = {};
    item.id = itemId;  //Id of item object that is being answered
    if(typeValue == "Skill")
        item.IdSelectedSkill = create_insert_item;  // Id of skill value selected
    if(typeValue == "Importance")
        item.IdSelectedImportance = create_insert_item; // Id of Importance value selected
    //Push new item with answers onto array
    answers.push(item);
}





/*************************************
    IMPORTANT REMINDER FOR NICOLAS!

**************************************/
//After we are able to succesfully send in the data we will need to create a way to check if all data is passed correctly.
