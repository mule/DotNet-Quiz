function QuestionEditor() {

    var questionText = '';
    var answerOptions = new Array();
    

    var me = this;

    this.QuestionText = questionText;
    this.AnswerOptions = answerOptions;
    this.AnswerType = 0;
    
    
    

    
        //Find needed resources from html
        var questionTxtBox = $('#txtQuestion');
        var answerOptionsBox = $('#AnswerOptions');
        var answerOptionTemplate = $('#answerOptionEditTemplate');
        var answerTypeDropDown = $('#AnswerType');
        var btnNext = $('#btnNext');

        wireEvents();




        QuestionEditor.prototype.CreateQuestion = function () {
            getQuestionText();
            getAnswerOptions();
            me.AnswerType = $(answerTypeDropDown).val();
            
            sendQuestionToServer();
        };

    QuestionEditor.prototype.AddAnswerOption = function () {

        answerOptionTemplate.tmpl().appendTo(answerOptionsBox);

    };

    function getQuestionText() {


        var questionTxt = questionTxtBox.val();
        me.QuestionText = questionTxt;

    }


    function getAnswerOptions() {

        me.AnswerOptions = new Array();
        
        var options = answerOptionsBox.find('.answerOption');
        options.each(function () {
            var id = $(this).attr('id');
            var answerTxt = $(this).find('.txtAnswerOption').val();
            var correctAnswer = $(this).find('.cbxCorrect').attr('checked');
            me.AnswerOptions.push({ 'id': id, 'AnswerText': answerTxt, 'correct': correctAnswer });
        });
    }

    function removeAnswerOption(optionBtn) {

        optionBtn.parent().remove();

    }

    function wireEvents() {
        answerOptionsBox.find('.removeAnswerBtn').live('click', function ()
        { removeAnswerOption($(this))});

        $('#btnAddAnswer').bind('click', function () { me.AddAnswerOption(); return false; });
        $('#btnCreateQuestion').bind('click', function() { me.CreateQuestion(); return false; });

    }


    function sendQuestionToServer() {

        $.ajax({
            url: "/Admin/AdminHome/CreateQuestion",
            type: "POST",
            data: $.toDictionary({ QuestionText: me.QuestionText, AnswerOptions: me.AnswerOptions, AnswerType: me.AnswerType }),
            datatype: "json",
            success: function (result) {

                if (result.item1 == true) {
                    alert(result.item2);     
                }

            },
            failure: function (result) {
                alert(result);
            }
        });

    }

}


function Question() {

    var id, questionText, answerType;
    this.AnswerOptions = new Array();
    this.Id = id;
    this.QuestionText = questionText;
    this.AnswerType = answerType;
    
   
}


function Quiz() {
    var quizId;
    var startTime;
    var host = '';
    //var currentQuestionId;

    this.CurrentQuestion = new Question();
    this.Host = host;
    var me = this;

    //page resouces
    var answerBox = $('#answerBox');
    var questionBox = $('#questionBox');
    var txtQuestion = questionBox.find('#question');
    var answerTemplate = $('#answerOptionsTemplate');
    var multiChoiceAnswer = $('#answerOptionsTemplateMultiChoice');
    var answerOptions = $('#answerOptions');
    var btnAnswerQuestion = $('#btnAnswer');
    var btnNextQuestion = $('#btnNextQuestion');

    wireEvents();
    
    

    Quiz.prototype.StartNewQuiz = function () {

        $.ajax({
            url: me.Host + "/Quiz/NewQuiz",
            type: "POST",
            datatype: "json",

            success: function (result) {
                quizId = result.QuizId;
                me.startTime = result.StartTime;
                me.GetQuestionFromServer();
            }

        });
    };

    Quiz.prototype.GetQuizStatus = function () {

        var id = $.cookie('DotNetQuiz');

        if (id != null) {

            this.quizId = parseInt(id);

            $.ajax({
                url: me.Host + "/Quiz/QuizStatus",
                type: "POST",
                data: { quizId: me.quizId },
                datatype: "json",
                success: function (result) {
                    me.startTime = result.StartTime;

                    me.GetQuestionFromServer();
                },
                failure: function (result) {
                    alert('Could not get Quiz status');
                }
            });
        } else {
            me.StartNewQuiz();
        }
    };


    Quiz.prototype.AnswerQuestion = function () {

        var answers = new Array();
        $('.answer input:[checked=true]').each(function () {

            var id = $(this).val();
            answers.push(id);

        });


        $.ajax({
            url: me.Host + "/Quiz/Answer",
            type: "POST",
            traditional: true,
            data: { question: me.CurrentQuestion.Id, answers: answers, quizId: me.quizId },
            dataType: "json",
            success: function (result) {
                $('#questionBox').hide();
                var answer = $('#answerBox');
                if (result.correct == false) {
                    answer.find('#succesBox').text("Answer was incorrect");
                    answer.find('#messageBox').text(result.message);
                } else {
                    answer.find('#succesBox').text("Answer was correct");
                    answer.find('#messageBox').text(result.message);
                }
                answer.show();

            }
        });
    };



    Quiz.prototype.GetQuestionFromServer = function() {

        $.ajax({
                url: me.Host +  "/Quiz/NextQuestion",
                type: "POST",
                data: quizId,
                dataType: "json",

                success: function(result) {
                    
                    me.CurrentQuestion.Id = result.Id;
                    me.CurrentQuestion.QuestionText = result.QuestionText;
                    me.CurrentQuestion.AnswerOptions = result.AnswerOptions;
                    me.CurrentQuestion.AnswerType = result.AnswerType;
                    me.RefreshQuestion();

                }

                //TODO: Add failure handling
            });
        };

        Quiz.prototype.RefreshQuestion = function () {

            if (answerBox != null && txtQuestion != null && answerOptions != null && answerTemplate != null && questionBox != null && multiChoiceAnswer != null) {
                answerBox.hide();
                txtQuestion.text(me.CurrentQuestion.QuestionText);
                answerOptions.empty();

                if (me.CurrentQuestion.AnswerType == 1)
                    answerTemplate.tmpl(me.CurrentQuestion.AnswerOptions).appendTo(answerOptions);

                if (me.CurrentQuestion.AnswerType == 2)
                    multiChoiceAnswer.tmpl(me.CurrentQuestion.AnswerOptions).appendTo(answerOptions);

                questionBox.show();
            }
            //TODO: Log error if resources not found

        };
    
    function wireEvents() {
        
        if(btnAnswerQuestion!=null && btnNextQuestion!=null) {
            btnAnswerQuestion.bind('click', function() { me.AnswerQuestion(); return false; });
            btnNextQuestion.bind('click', function() { me.GetQuestionFromServer(); return false; });
        }


    }

}







/*!

* jQuery toDictionary() plugin

*

* Version 1.2 (11 Apr 2011)

*

* Copyright (c) 2011 Robert Koritnik

* Licensed under the terms of the MIT license

* http://www.opensource.org/licenses/mit-license.php

*/



(function ($) {



    // #region String.prototype.format

    // add String prototype format function if it doesn't yet exist

    if ($.isFunction(String.prototype.format) === false) {

        String.prototype.format = function () {

            var s = this;

            var i = arguments.length;

            while (i--) {

                s = s.replace(new RegExp("\\{" + i + "\\}", "gim"), arguments[i]);

            }

            return s;

        };

    }

    // #endregion



    // #region Date.prototype.toISOString

    // add Date prototype toISOString function if it doesn't yet exist

    if ($.isFunction(Date.prototype.toISOString) === false) {

        Date.prototype.toISOString = function () {

            var pad = function (n, places) {

                n = n.toString();

                for (var i = n.length; i < places; i++) {

                    n = "0" + n;

                }

                return n;

            };

            var d = this;

            return "{0}-{1}-{2}T{3}:{4}:{5}.{6}Z".format(

                d.getUTCFullYear(),

                pad(d.getUTCMonth() + 1, 2),

                pad(d.getUTCDate(), 2),

                pad(d.getUTCHours(), 2),

                pad(d.getUTCMinutes(), 2),

                pad(d.getUTCSeconds(), 2),

                pad(d.getUTCMilliseconds(), 3)

            );

        };

    }

    // #endregion



    var _flatten = function (input, output, prefix, includeNulls) {

        if ($.isPlainObject(input)) {

            for (var p in input) {

                if (includeNulls === true || typeof (input[p]) !== "undefined" && input[p] !== null) {

                    _flatten(input[p], output, prefix.length > 0 ? prefix + "." + p : p, includeNulls);

                }

            }

        }

        else {

            if ($.isArray(input)) {

                $.each(input, function (index, value) {

                    _flatten(value, output, "{0}[{1}]".format(prefix, index));

                });

                return;

            }

            if (!$.isFunction(input)) {

                if (input instanceof Date) {

                    output.push({ name: prefix, value: input.toISOString() });

                }

                else {

                    var val = typeof (input);

                    switch (val) {

                        case "boolean":

                        case "number":

                            val = input;

                            break;

                        case "object":

                            // this property is null, because non-null objects are evaluated in first if branch

                            if (includeNulls !== true) {

                                return;

                            }

                        default:

                            val = input || "";

                    }

                    output.push({ name: prefix, value: val });

                }

            }

        }

    };



    $.extend({

        toDictionary: function (data, prefix, includeNulls) {

            /// <summary>Flattens an arbitrary JSON object to a dictionary that Asp.net MVC default model binder understands.</summary>

            /// <param name="data" type="Object">Can either be a JSON object or a function that returns one.</data>

            /// <param name="prefix" type="String" Optional="true">Provide this parameter when you want the output names to be prefixed by something (ie. when flattening simple values).</param>

            /// <param name="includeNulls" type="Boolean" Optional="true">Set this to 'true' when you want null valued properties to be included in result (default is 'false').</param>



            // get data first if provided parameter is a function

            data = $.isFunction(data) ? data.call() : data;



            // is second argument "prefix" or "includeNulls"

            if (arguments.length === 2 && typeof (prefix) === "boolean") {

                includeNulls = prefix;

                prefix = "";

            }



            // set "includeNulls" default

            includeNulls = typeof (includeNulls) === "boolean" ? includeNulls : false;



            var result = [];

            _flatten(data, result, prefix || "", includeNulls);



            return result;

        }

    });

})(jQuery);

