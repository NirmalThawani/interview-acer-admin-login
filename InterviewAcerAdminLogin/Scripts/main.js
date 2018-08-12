// for accordian arrow up and down

// $('.panel-collapse').on('show.bs.collapse', function () {
//     $(this).siblings('.panel-heading').addClass('active');
//   });

//   $('.panel-collapse').on('hide.bs.collapse', function () {
//     $(this).siblings('.panel-heading').removeClass('active');
//   });

$('.panel-heading a').click(function() {
  $('.panel-heading').removeClass('active');
  
  //If the panel was open and would be closed by this click, do not active it
  if(!$(this).closest('.panel').find('.panel-collapse').hasClass('in'))
      $(this).parents('.panel-heading').addClass('active');
});


  // for more dots display

//   $('.moreDots-wrapper').click(function(){
//     if($('.moreDots-content').css('display') == 'none')
//     {
//       $('.moreDots-content').show();
//     }
//     else
//     {
//       $('.moreDots-content').hide();
      
//     } 
// });



 