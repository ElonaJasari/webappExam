document.addEventListener('DOMContentLoaded', function() {
  console.log('New user page loaded');
  
  // Optional: Add fade-in animation
  const content = document.querySelector('.welcome-content');
  if (content) {
    content.style.opacity = '0';
    content.style.transform = 'translateY(20px)';
    
    setTimeout(() => {
      content.style.transition = 'all 0.6s ease';
      content.style.opacity = '1';
      content.style.transform = 'translateY(0)';
    }, 100);
  }
});