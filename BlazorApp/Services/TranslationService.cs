namespace XiloWeb.Services;

public class TranslationService
{
    private string _currentLanguage = "en";
    public string CurrentLanguage => _currentLanguage;
    public event Action? OnLanguageChanged;

    private readonly Dictionary<string, Dictionary<string, string>> _translations = new()
    {
        ["en"] = new()
        {
            ["nav-contact"] = "Contact us",
            ["hero-title-line1"] = "WE CREATE. WE CONNECT.",
            ["hero-title-line2"] = "WE INVEST IN WHAT MATTERS.",
            ["hero-text"] = "XILO is where our companies, initiatives, and community come together. It's a living space designed to build, collaborate, and support ideas that move people forward.",
            ["hero-cta"] = "Partner with us",
            ["statement-p1"] = "We build companies that solve real problems. We launch initiatives that spark new conversations. We partner with communities who share our core belief: technology should serve people, not the other way around.",
            ["statement-p2"] = "Everything we do connects back to one idea: bringing together the right people, the right tools, and vision to create impact that lasts.",
            ["companies-title"] = "OUR COMPANIES",
            ["xipe-tagline"] = "SOFTWARE, BUILT WITH AI AT ITS CORE",
            ["xipe-description"] = "Xipe is our technology foundation. We build software by integrating AI across the entire development lifecycle, from early validation to production systems. The result is reliable, scalable products delivered with less friction and more clarity.",
            ["xipe-cta"] = "See how Xipe works",
            ["diana-tagline"] = "AN INTELLIGENCE LAYER FOR BUILDING SOFTWARE",
            ["diana-description"] = "Diana is our proprietary AI system that transforms how software gets created. It generates production-ready code, structures architectures, and accelerates decision-making, compressing timelines from months into days.",
            ["diana-cta"] = "Meet Diana",
            ["xilo-tagline"] = "WEBSITES, BUILT AT THE SPEED OF AI",
            ["xilo-description"] = "Xilo Design is our AI-powered web studio. We design and launch high-performance websites that combine strategy, design, and AI-accelerated development; so businesses can go from idea to live site faster than ever.",
            ["xilo-cta"] = "Build your website with Xilo",
            ["demoday-title"] = "DEMO DAY",
            ["demoday-description"] = "The stage for creators. A recurrent event where builders showcase their work, share learning, and connect with the community.",
            ["demoday-showcase"] = "Showcase your project",
            ["demoday-join"] = "Join the next demo day",
            ["howit-title"] = "HOW IT SHIFTS",
            ["howit-cta"] = "Explore partnerships",
            ["howit-description1"] = "Our podcast and digital magazine highlighting conversations, ideas, and perspectives from the frontlines of technology. We sit down with founders, builders, and thinkers who are shifting paradigms and thinking about what comes next.",
            ["howit-description2"] = "Alongside our conversations, we share articles, opinion pieces, and features that dig into the realities of building, leading, and navigating new waves of industry.",
            ["howit-tagline"] = "Real stories. Real lessons. Real perspectives.",
            ["partner-cta-title"] = "LOOKING FOR A PARTNER FOR YOUR TECH PROJECT?",
            ["partner-cta-subtitle"] = "We support projects by contributing full-stack design, strategy, and execution, aligning long-term vision with real-world delivery. We put our team beside yours and work using our knowledge and our hands-on work.",
            ["partner-cta-btn"] = "Start a conversation",
            ["spotlight-heading"] = "SPOTLIGHT PARTNERSHIP",
            ["spotlight-label"] = "XILO PARTNERS",
            ["spotlight-name"] = "CHOP LOCAL",
            ["spotlight-description"] = "We've been part of the ChopLocal journey since the beginning, providing the technological backbone to help them transform how local business connect with people.",
            ["spotlight-cta"] = "Discover Chop Local",
            ["community-title"] = "WE DON'T BUILD ALONE",
            ["community-subtitle"] = "We partner with companies and local communities to create better outcomes",
            ["contact-title"] = "LET'S TALK",
            ["contact-subtitle"] = "Whether you want to showcase your project at Demo Day, explore a partnership, or just start a conversation, we'd love to hear from you.",
            ["contact-inquiry-placeholder"] = "What are you looking for?",
            ["contact-inquiry-showcase"] = "Showcase project at demo day",
            ["contact-inquiry-partner"] = "Partner with us",
            ["contact-inquiry-general"] = "Start a conversation",
            ["contact-name-placeholder"] = "Name",
            ["contact-email-placeholder"] = "Email",
            ["contact-company-placeholder"] = "Company",
            ["contact-message-placeholder"] = "Tell us more about what you have in mind",
            ["contact-submit"] = "Send message",
            ["contact-success"] = "Thank you! We'll be in touch soon.",
            ["footer-companies"] = "Companies",
            ["footer-initiatives"] = "Initiatives",
            ["footer-partnerships"] = "Partnerships",
            ["footer-contact-label"] = "Contact",
            ["footer-follow"] = "Follow us on social media",
            ["modal-title"] = "Next Demo Day",
            ["modal-no-events"] = "There are no events scheduled at the moment. Follow us on social media to stay updated on upcoming Demo Days.",
            ["modal-event-text"] = "Our next Demo Day is coming up. Reserve your spot!",
            ["modal-register"] = "Register on Luma",
            ["modal-close"] = "Close",
            ["scroll-top"] = "↑",
        },
        ["es"] = new()
        {
            ["nav-contact"] = "Contáctanos",
            ["hero-title-line1"] = "CREAMOS. CONECTAMOS.",
            ["hero-title-line2"] = "INVERTIMOS EN LO QUE IMPORTA.",
            ["hero-text"] = "XILO es donde nuestras empresas, iniciativas y comunidad se unen. Es un espacio vivo diseñado para construir, colaborar y apoyar ideas que impulsan a las personas.",
            ["hero-cta"] = "Colabora con nosotros",
            ["statement-p1"] = "Construimos empresas que resuelven problemas reales. Lanzamos iniciativas que generan nuevas conversaciones. Nos asociamos con comunidades que comparten nuestra creencia: la tecnología debe servir a las personas, no al revés.",
            ["statement-p2"] = "Todo lo que hacemos se conecta con una idea: reunir a las personas correctas, las herramientas adecuadas y la visión para crear impacto duradero.",
            ["companies-title"] = "NUESTRAS EMPRESAS",
            ["xipe-tagline"] = "SOFTWARE, CONSTRUIDO CON IA EN SU NÚCLEO",
            ["xipe-description"] = "Xipe es nuestra base tecnológica. Construimos software integrando IA en todo el ciclo de desarrollo, desde la validación temprana hasta los sistemas en producción. El resultado: productos confiables y escalables, entregados con menos fricción y más claridad.",
            ["xipe-cta"] = "Descubre cómo funciona Xipe",
            ["diana-tagline"] = "UNA CAPA DE INTELIGENCIA PARA CONSTRUIR SOFTWARE",
            ["diana-description"] = "Diana es nuestro sistema de IA propietario que transforma cómo se crea el software. Genera código listo para producción, estructura arquitecturas y acelera la toma de decisiones, comprimiendo los plazos de meses a días.",
            ["diana-cta"] = "Conoce a Diana",
            ["xilo-tagline"] = "SITIOS WEB, A LA VELOCIDAD DE LA IA",
            ["xilo-description"] = "Xilo Design es nuestro estudio web impulsado por IA. Diseñamos y lanzamos sitios web de alto rendimiento que combinan estrategia, diseño y desarrollo acelerado por IA; para que las empresas pasen de la idea al sitio en línea más rápido que nunca.",
            ["xilo-cta"] = "Construye tu sitio web con Xilo",
            ["demoday-title"] = "DEMO DAY",
            ["demoday-description"] = "El escenario para creadores. Un evento recurrente donde los constructores muestran su trabajo, comparten aprendizajes y se conectan con la comunidad.",
            ["demoday-showcase"] = "Presenta tu proyecto",
            ["demoday-join"] = "Únete al próximo demo day",
            ["howit-title"] = "HOW IT SHIFTS",
            ["howit-cta"] = "Explora alianzas",
            ["howit-description1"] = "Nuestro podcast y revista digital destacando conversaciones, ideas y perspectivas desde la primera línea de la tecnología. Nos sentamos con fundadores, constructores y pensadores que están cambiando paradigmas y pensando en lo que viene.",
            ["howit-description2"] = "Junto a nuestras conversaciones, compartimos artículos, opiniones y reportajes que profundizan en las realidades de construir, liderar y navegar nuevas olas de la industria.",
            ["howit-tagline"] = "Historias reales. Lecciones reales. Perspectivas reales.",
            ["partner-cta-title"] = "¿BUSCAS UN SOCIO PARA TU PROYECTO TECNOLÓGICO?",
            ["partner-cta-subtitle"] = "Apoyamos proyectos contribuyendo diseño full-stack, estrategia y ejecución, alineando la visión a largo plazo con la entrega real. Ponemos a nuestro equipo junto al tuyo y trabajamos con nuestro conocimiento y manos a la obra.",
            ["partner-cta-btn"] = "Inicia una conversación",
            ["spotlight-heading"] = "ALIANZA DESTACADA",
            ["spotlight-label"] = "SOCIOS XILO",
            ["spotlight-name"] = "CHOP LOCAL",
            ["spotlight-description"] = "Hemos sido parte del camino de ChopLocal desde el inicio, proporcionando la columna tecnológica para ayudarlos a transformar cómo los negocios locales conectan con las personas.",
            ["spotlight-cta"] = "Descubre Chop Local",
            ["community-title"] = "NO CONSTRUIMOS SOLOS",
            ["community-subtitle"] = "Nos asociamos con empresas y comunidades locales para crear mejores resultados",
            ["contact-title"] = "HABLEMOS",
            ["contact-subtitle"] = "Ya sea que quieras presentar tu proyecto en Demo Day, explorar una alianza, o simplemente iniciar una conversación, nos encantaría saber de ti.",
            ["contact-inquiry-placeholder"] = "¿Qué estás buscando?",
            ["contact-inquiry-showcase"] = "Presentar proyecto en demo day",
            ["contact-inquiry-partner"] = "Colaborar con nosotros",
            ["contact-inquiry-general"] = "Iniciar una conversación",
            ["contact-name-placeholder"] = "Nombre",
            ["contact-email-placeholder"] = "Correo electrónico",
            ["contact-company-placeholder"] = "Empresa",
            ["contact-message-placeholder"] = "Cuéntanos más sobre lo que tienes en mente",
            ["contact-submit"] = "Enviar mensaje",
            ["contact-success"] = "¡Gracias! Nos pondremos en contacto pronto.",
            ["footer-companies"] = "Empresas",
            ["footer-initiatives"] = "Iniciativas",
            ["footer-partnerships"] = "Alianzas",
            ["footer-contact-label"] = "Contacto",
            ["footer-follow"] = "Síguenos en redes sociales",
            ["modal-title"] = "Próximo Demo Day",
            ["modal-no-events"] = "No hay eventos programados en este momento. Síguenos en redes sociales para enterarte de los próximos Demo Days.",
            ["modal-event-text"] = "Nuestro próximo Demo Day está por llegar. ¡Reserva tu lugar!",
            ["modal-register"] = "Regístrate en Luma",
            ["modal-close"] = "Cerrar",
            ["scroll-top"] = "↑",
        }
    };

    public string T(string key) =>
        _translations.TryGetValue(_currentLanguage, out var dict) && dict.TryGetValue(key, out var val)
            ? val : key;

    public void SetLanguage(string lang)
    {
        if (lang != _currentLanguage)
        {
            _currentLanguage = lang;
            OnLanguageChanged?.Invoke();
        }
    }
}
