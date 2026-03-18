/* =========================================
   XILO VENTURE STUDIO — Main Script
   ========================================= */

document.addEventListener('DOMContentLoaded', () => {

  /* ---------- Mobile Nav Toggle ---------- */
  const toggle = document.querySelector('.nav-toggle');
  const navLinks = document.querySelector('.nav-links');

  if (toggle) {
    toggle.addEventListener('click', () => {
      navLinks.classList.toggle('active');
      toggle.classList.toggle('open');
    });

    // Close menu when a link is clicked
    navLinks.querySelectorAll('a').forEach(link => {
      link.addEventListener('click', () => {
        navLinks.classList.remove('active');
        toggle.classList.remove('open');
      });
    });

    // Close menu when clicking outside
    document.addEventListener('click', (e) => {
      if (!toggle.contains(e.target) && !navLinks.contains(e.target)) {
        navLinks.classList.remove('active');
        toggle.classList.remove('open');
      }
    });
  }

  /* ---------- Navbar shrink on scroll ---------- */
  const navbar = document.querySelector('.navbar');
  window.addEventListener('scroll', () => {
    if (window.scrollY > 60) {
      navbar.classList.add('scrolled');
    } else {
      navbar.classList.remove('scrolled');
    }
  });

  /* ---------- Fade-in on scroll (Intersection Observer) ---------- */
  const faders = document.querySelectorAll(
    '.hero-content, .hero-image, .statement-large, .company-card, .initiative-card, .demoday-top, .demoday-gallery, .podcast-top, .podcast-gallery, .partner-container, .spotlight-heading, .spotlight-inner, .news-card, .community-container'
  );

  const appearOptions = {
    threshold: 0.15,
    rootMargin: '0px 0px -40px 0px'
  };

  const appearOnScroll = new IntersectionObserver((entries, observer) => {
    entries.forEach(entry => {
      if (entry.isIntersecting) {
        entry.target.classList.add('appear');
        observer.unobserve(entry.target);
      }
    });
  }, appearOptions);

  faders.forEach(fader => {
    fader.classList.add('fade-in');
    appearOnScroll.observe(fader);
  });

  /* ---------- Smooth scroll for anchor links ---------- */
  document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
      const target = document.querySelector(this.getAttribute('href'));
      if (target) {
        e.preventDefault();
        target.scrollIntoView({ behavior: 'smooth', block: 'start' });
      }
    });
  });

  /* ---------- Scroll to top ---------- */
  const scrollTopBtn = document.querySelector('.footer-scroll-top');
  if (scrollTopBtn) {
    scrollTopBtn.addEventListener('click', (e) => {
      e.preventDefault();
      window.scrollTo({ top: 0, behavior: 'smooth' });
    });
  }

  /* ---------- Podcast Gallery Slider ---------- */
  const podcastGallery = document.querySelector('.podcast-gallery');
  const prevBtn = document.querySelector('.podcast-arrow-prev');
  const nextBtn = document.querySelector('.podcast-arrow-next');

  if (podcastGallery && prevBtn && nextBtn) {
    let currentPage = 0;
    const totalItems = podcastGallery.children.length;
    const itemsPerPage = 3;
    const totalPages = Math.ceil(totalItems / itemsPerPage);

    function updateSlider() {
      const firstItem = podcastGallery.children[0];
      const gap = parseFloat(getComputedStyle(podcastGallery).gap) || 0;
      const itemWidth = firstItem.offsetWidth + gap;
      const scrollAmount = currentPage * itemsPerPage * itemWidth;
      podcastGallery.style.transform = `translateX(-${scrollAmount}px)`;
      prevBtn.disabled = currentPage === 0;
      nextBtn.disabled = currentPage >= totalPages - 1;
    }

    window.addEventListener('resize', () => updateSlider());

    prevBtn.addEventListener('click', () => {
      if (currentPage > 0) {
        currentPage--;
        updateSlider();
      }
    });

    nextBtn.addEventListener('click', () => {
      if (currentPage < totalPages - 1) {
        currentPage++;
        updateSlider();
      }
    });

    updateSlider();
  }

  /* ---------- Join Modal (Demo Day) ---------- */

  // Config: set lumaUrl to a real Luma link when there's an event, or null/empty for no event
  const lumaUrl = null; // e.g. "https://lu.ma/your-event-id"

  const joinModal = document.getElementById('join-modal');
  const joinBtn = document.getElementById('join-btn');

  function openModal(modal) {
    modal.classList.add('active');
    document.body.style.overflow = 'hidden';
  }

  function closeModal(modal) {
    modal.classList.remove('active');
    document.body.style.overflow = '';
  }

  // Close on overlay click or X button
  document.querySelectorAll('.modal-overlay').forEach(overlay => {
    overlay.addEventListener('click', (e) => {
      if (e.target === overlay) closeModal(overlay);
    });
    const closeBtn = overlay.querySelector('.modal-close');
    if (closeBtn) {
      closeBtn.addEventListener('click', () => closeModal(overlay));
    }
  });

  // Close on Escape key
  document.addEventListener('keydown', (e) => {
    if (e.key === 'Escape') {
      document.querySelectorAll('.modal-overlay.active').forEach(closeModal);
    }
  });

  // Join modal — shows Luma link or "no events" message
  if (joinBtn) {
    joinBtn.addEventListener('click', () => {
      const joinContent = document.getElementById('join-content');
      const isEs = currentLang === 'es';
      if (lumaUrl) {
        joinContent.innerHTML = `
          <p>${isEs ? 'Nuestro próximo Demo Day está por llegar. ¡Reserva tu lugar!' : 'Our next Demo Day is coming up. Reserve your spot!'}</p>
          <a href="${lumaUrl}" target="_blank" class="btn btn-demoday">${isEs ? 'Regístrate en Luma' : 'Register on Luma'}</a>
        `;
      } else {
        joinContent.innerHTML = `
          <p>${isEs ? 'No hay eventos programados en este momento. Síguenos en redes sociales para enterarte de los próximos Demo Days.' : 'There are no events scheduled at the moment. Follow us on social media to stay updated on upcoming Demo Days.'}</p>
        `;
      }
      // Update modal title
      const modalTitle = joinModal.querySelector('.modal-title');
      if (modalTitle) modalTitle.textContent = isEs ? 'Próximo Demo Day' : 'Next Demo Day';
      openModal(joinModal);
    });
  }

  /* ---------- Language Switcher (i18n) ---------- */
  const translations = {
    en: {
      nav_contact: 'Contact us',
      hero_title: 'WE CREATE. WE CONNECT.<br>WE INVEST IN WHAT MATTERS.',
      hero_text: "XILO is where our companies, initiatives, and community come together. It's a living space designed to build, collaborate, and support ideas that move people forward.",
      hero_cta: 'Partner with us',
      statement_1: 'We build companies that solve real problems. We launch initiatives that spark new conversations. We partner with communities who share our core belief: technology should serve people, not the other way around.',
      statement_2: 'Everything we do connects back to one idea: bringing together the right people, the right tools, and vision to create impact that lasts.',
      companies_title: 'OUR COMPANIES',
      xipe_tagline: 'SOFTWARE, BUILT WITH AI AT ITS CORE',
      xipe_desc: 'Xipe is our technology foundation. We build software by integrating AI across the entire development lifecycle, from early validation to production systems. The result is reliable, scalable products delivered with less friction and more clarity.',
      xipe_cta: 'See how Xipe works',
      diana_tagline: 'AN INTELLIGENCE LAYER FOR BUILDING SOFTWARE',
      diana_desc: 'Diana is our proprietary AI system that transforms how software gets created. It generates production-ready code, structures architectures, and accelerates decision-making, compressing timelines from months into days.',
      diana_cta: 'Meet Diana',
      xilo_tagline: 'WEBSITES, BUILT AT THE SPEED OF AI',
      xilo_desc: 'Xilo Design is our AI-powered web studio. We design and launch high-performance websites that combine strategy, design, and AI-accelerated development; so businesses can go from idea to live site faster than ever.',
      xilo_cta: 'Build your website with Xilo',
      demoday_desc: 'The stage for creators. A recurrent event where builders showcase their work, share learning, and connect with the community.',
      demoday_showcase: 'Showcase your project',
      demoday_join: 'Join the next demo day',
      podcast_cta: 'Explore partnerships',
      podcast_desc1: 'Our podcast and digital magazine highlighting conversations, ideas, and perspectives from the frontlines of technology. We sit down with founders, builders, and thinkers who are shifting paradigms and thinking about what comes next.',
      podcast_desc2: 'Alongside our conversations, we share articles, opinion pieces, and features that dig into the realities of building, leading, and navigating new waves of industry.',
      podcast_tagline: 'Real stories. Real lessons. Real perspectives.',
      partner_title: 'LOOKING FOR A PARTNER FOR YOUR TECH PROJECT?',
      partner_desc: 'We support projects by contributing full-stack design, strategy, and execution, aligning long-term vision with real-world delivery. We put our team beside yours and work using our knowledge and our hands-on work.',
      partner_cta: 'Start a conversation',
      spotlight_heading: 'SPOTLIGHT PARTNERSHIP',
      spotlight_label: 'XILO PARTNERS',
      spotlight_desc: "We've been part of the ChopLocal journey since the beginning, providing the technological backbone to help them transform how local business connect with people.",
      spotlight_cta: 'Discover Chop Local',
      community_title: "WE DON'T BUILD ALONE",
      community_desc: '<em>We partner with companies and local communities to create better outcomes</em>',
      contact_title: "LET'S TALK",
      contact_desc: "Whether you want to showcase your project at Demo Day, explore a partnership, or just start a conversation, we'd love to hear from you.",
      form_select: 'What are you looking for?',
      form_showcase: 'Showcase project at demo day',
      form_partner: 'Partner with us',
      form_general: 'Start a conversation',
      form_name: 'Name',
      form_email: 'Email',
      form_company: 'Company',
      form_message: 'Tell us more about what you have in mind',
      form_submit: 'Send message',
      form_success: "Thank you! We'll be in touch soon.",
      footer_contact: 'Contact',
      footer_follow: 'Follow us on social media',
      footer_companies: 'Companies',
      footer_initiatives: 'Initiatives',
      footer_partnerships: 'Partnerships'
    },
    es: {
      nav_contact: 'Contáctanos',
      hero_title: 'CREAMOS. CONECTAMOS.<br>INVERTIMOS EN LO QUE IMPORTA.',
      hero_text: 'XILO es donde nuestras empresas, iniciativas y comunidad se unen. Es un espacio vivo diseñado para construir, colaborar y apoyar ideas que impulsan a las personas.',
      hero_cta: 'Colabora con nosotros',
      statement_1: 'Construimos empresas que resuelven problemas reales. Lanzamos iniciativas que generan nuevas conversaciones. Nos asociamos con comunidades que comparten nuestra creencia: la tecnología debe servir a las personas, no al revés.',
      statement_2: 'Todo lo que hacemos se conecta con una idea: reunir a las personas correctas, las herramientas adecuadas y la visión para crear impacto duradero.',
      companies_title: 'NUESTRAS EMPRESAS',
      xipe_tagline: 'SOFTWARE, CONSTRUIDO CON IA EN SU NÚCLEO',
      xipe_desc: 'Xipe es nuestra base tecnológica. Construimos software integrando IA en todo el ciclo de desarrollo, desde la validación temprana hasta los sistemas en producción. El resultado: productos confiables y escalables, entregados con menos fricción y más claridad.',
      xipe_cta: 'Descubre cómo funciona Xipe',
      diana_tagline: 'UNA CAPA DE INTELIGENCIA PARA CONSTRUIR SOFTWARE',
      diana_desc: 'Diana es nuestro sistema propietario de IA que transforma cómo se crea el software. Genera código listo para producción, estructura arquitecturas y acelera la toma de decisiones, comprimiendo plazos de meses a días.',
      diana_cta: 'Conoce a Diana',
      xilo_tagline: 'SITIOS WEB, A LA VELOCIDAD DE LA IA',
      xilo_desc: 'Xilo Design es nuestro estudio web impulsado por IA. Diseñamos y lanzamos sitios web de alto rendimiento que combinan estrategia, diseño y desarrollo acelerado por IA; para que las empresas pasen de la idea al sitio en línea más rápido que nunca.',
      xilo_cta: 'Construye tu sitio web con Xilo',
      demoday_desc: 'El escenario para creadores. Un evento recurrente donde los constructores muestran su trabajo, comparten aprendizajes y se conectan con la comunidad.',
      demoday_showcase: 'Presenta tu proyecto',
      demoday_join: 'Únete al próximo demo day',
      podcast_cta: 'Explora alianzas',
      podcast_desc1: 'Nuestro podcast y revista digital destacando conversaciones, ideas y perspectivas desde la primera línea de la tecnología. Nos sentamos con fundadores, constructores y pensadores que están cambiando paradigmas y pensando en lo que viene.',
      podcast_desc2: 'Junto a nuestras conversaciones, compartimos artículos, opiniones y reportajes que profundizan en las realidades de construir, liderar y navegar nuevas olas de la industria.',
      podcast_tagline: 'Historias reales. Lecciones reales. Perspectivas reales.',
      partner_title: '¿BUSCAS UN SOCIO PARA TU PROYECTO TECNOLÓGICO?',
      partner_desc: 'Apoyamos proyectos contribuyendo diseño full-stack, estrategia y ejecución, alineando la visión a largo plazo con la entrega real. Ponemos a nuestro equipo junto al tuyo y trabajamos con nuestro conocimiento y manos a la obra.',
      partner_cta: 'Inicia una conversación',
      spotlight_heading: 'ALIANZA DESTACADA',
      spotlight_label: 'SOCIOS XILO',
      spotlight_desc: 'Hemos sido parte del camino de ChopLocal desde el inicio, proporcionando la columna tecnológica para ayudarlos a transformar cómo los negocios locales conectan con las personas.',
      spotlight_cta: 'Descubre Chop Local',
      community_title: 'NO CONSTRUIMOS SOLOS',
      community_desc: '<em>Nos asociamos con empresas y comunidades locales para crear mejores resultados</em>',
      contact_title: 'HABLEMOS',
      contact_desc: 'Ya sea que quieras presentar tu proyecto en Demo Day, explorar una alianza, o simplemente iniciar una conversación, nos encantaría saber de ti.',
      form_select: '¿Qué estás buscando?',
      form_showcase: 'Presentar proyecto en demo day',
      form_partner: 'Colaborar con nosotros',
      form_general: 'Iniciar una conversación',
      form_name: 'Nombre',
      form_email: 'Correo electrónico',
      form_company: 'Empresa',
      form_message: 'Cuéntanos más sobre lo que tienes en mente',
      form_submit: 'Enviar mensaje',
      form_success: '¡Gracias! Nos pondremos en contacto pronto.',
      footer_contact: 'Contacto',
      footer_follow: 'Síguenos en redes sociales',
      footer_companies: 'Empresas',
      footer_initiatives: 'Iniciativas',
      footer_partnerships: 'Alianzas'
    }
  };

  let currentLang = 'en';

  function setLanguage(lang) {
    currentLang = lang;
    const dict = translations[lang];
    if (!dict) return;

    // Update text content
    document.querySelectorAll('[data-i18n]').forEach(el => {
      const key = el.getAttribute('data-i18n');
      if (dict[key] !== undefined) {
        el.innerHTML = dict[key];
      }
    });

    // Update placeholders
    document.querySelectorAll('[data-i18n-placeholder]').forEach(el => {
      const key = el.getAttribute('data-i18n-placeholder');
      if (dict[key] !== undefined) {
        el.placeholder = dict[key];
      }
    });

    // Update lang-btn active states (both navbar and footer)
    document.querySelectorAll('.lang-btn').forEach(btn => {
      btn.classList.toggle('active', btn.getAttribute('data-lang') === lang);
    });

    // Update html lang attribute
    document.documentElement.lang = lang === 'es' ? 'es' : 'en';
  }

  // Attach click listeners to all lang-btn elements (navbar + footer)
  document.querySelectorAll('.lang-btn').forEach(btn => {
    btn.addEventListener('click', () => {
      const lang = btn.getAttribute('data-lang');
      if (lang && lang !== currentLang) {
        setLanguage(lang);
      }
    });
  });

  /* ---------- Contact Section Form ---------- */
  const contactForm = document.getElementById('contact-form');
  const contactSuccess = document.getElementById('contact-success');
  const contactInquiry = document.getElementById('contact-inquiry');

  // Pre-select dropdown when clicking links with data-inquiry
  document.querySelectorAll('[data-inquiry]').forEach(link => {
    link.addEventListener('click', () => {
      const value = link.getAttribute('data-inquiry');
      if (contactInquiry && value) {
        contactInquiry.value = value;
      }
    });
  });

  if (contactForm) {
    contactForm.addEventListener('submit', (e) => {
      e.preventDefault();
      // TODO: send form data to backend
      contactForm.style.display = 'none';
      contactSuccess.style.display = 'block';
    });
  }

});
