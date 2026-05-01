/**
 * EZPos-System Installer Download Website
 * Main JavaScript file for interactive functionality
 */

// GitHub Repository Configuration
const GITHUB_REPO = 'Reef-hash/EZPos-System';
const GITHUB_API_URL = `https://api.github.com/repos/${GITHUB_REPO}/releases/latest`;
const INSTALLER_FILENAME = 'SetupEZPos.exe';

// Global variable to store download URL
let currentDownloadUrl = null;

/**
 * Fetch the latest release information from GitHub
 */
async function fetchLatestRelease() {
    try {
        const response = await fetch(GITHUB_API_URL);
        
        if (!response.ok) {
            throw new Error(`GitHub API error: ${response.status}`);
        }
        
        const releaseData = await response.json();
        
        // Find the installer asset
        const installerAsset = releaseData.assets.find(asset => 
            asset.name.includes(INSTALLER_FILENAME.split('.')[0])
        );
        
        if (!installerAsset) {
            throw new Error(`${INSTALLER_FILENAME} not found in latest release`);
        }
        
        // Extract version from tag (e.g., v2.1.0 -> 2.1.0)
        const version = releaseData.tag_name.replace(/^v/, '');
        const fileSize = (installerAsset.size / (1024 * 1024)).toFixed(1);
        const releaseDate = new Date(releaseData.published_at).toLocaleDateString('en-US', {
            year: 'numeric',
            month: 'short',
            day: 'numeric'
        });
        
        // Update download URL
        currentDownloadUrl = installerAsset.browser_download_url;
        
        // Update UI
        document.getElementById('versionDisplay').textContent = `Version ${version}`;
        document.getElementById('sizeDisplay').textContent = `File size: ${fileSize} MB`;
        document.getElementById('dateDisplay').textContent = `Released: ${releaseDate}`;
        
        // Enable download button
        const downloadBtn = document.getElementById('downloadBtn');
        downloadBtn.disabled = false;
        downloadBtn.querySelector('.download-text').textContent = 'Download Now';
        
        console.log(`✅ Latest release fetched: ${version}`);
        
    } catch (error) {
        console.error('Error fetching latest release:', error);
        
        // Show error state
        document.getElementById('versionDisplay').textContent = 'Version: Unable to fetch';
        document.getElementById('sizeDisplay').textContent = 'File size: Check releases page';
        document.getElementById('dateDisplay').textContent = 'Visit GitHub for latest release';
        
        const downloadBtn = document.getElementById('downloadBtn');
        downloadBtn.querySelector('.download-text').textContent = 'Open Releases Page';
        downloadBtn.disabled = false;
        downloadBtn.onclick = () => window.open(`https://github.com/${GITHUB_REPO}/releases`, '_blank');
    }
}

/**
 * Handle installer download
 */
function downloadInstaller() {
    if (!currentDownloadUrl) {
        window.open(`https://github.com/${GITHUB_REPO}/releases`, '_blank');
        return;
    }
    
    const releaseVersion = document.getElementById('versionDisplay').textContent;
    const releaseDate = document.getElementById('dateDisplay').textContent;
    
    // Show modal
    const modal = document.getElementById('downloadModal');
    const platformMessage = document.getElementById('platformMessage');
    const fallbackLink = document.getElementById('fallbackLink');
    
    platformMessage.textContent = `Downloading EZPos-System ${releaseVersion}...`;
    fallbackLink.href = currentDownloadUrl;
    
    modal.style.display = 'block';
    
    // Trigger actual download
    setTimeout(() => {
        const element = document.createElement('a');
        element.href = currentDownloadUrl;
        element.download = '';
        document.body.appendChild(element);
        element.click();
        document.body.removeChild(element);
    }, 500);
    
    // Track download
    trackDownload(releaseVersion);
}

/**
 * Close the download modal
 */
function closeModal() {
    const modal = document.getElementById('downloadModal');
    modal.style.display = 'none';
}

/**
 * Scroll to download section
 */
function scrollToDownload() {
    const downloadSection = document.getElementById('download');
    downloadSection.scrollIntoView({ behavior: 'smooth' });
}

/**
 * Track download statistics
 * @param {string} version - The version being downloaded
 */
function trackDownload(version) {
    // This can be expanded to send analytics to a server
    console.log(`📥 Download started: ${version}`);
    console.log(`⏰ Timestamp: ${new Date().toISOString()}`);
}

/**
 * Initialize smooth scroll behavior for navigation links
 */
function initializeSmoothScroll() {
    const navLinks = document.querySelectorAll('a[href^="#"]');
    
    navLinks.forEach(link => {
        link.addEventListener('click', function(e) {
            const href = this.getAttribute('href');
            
            // Skip if it's not a valid anchor
            if (href === '#') return;
            
            e.preventDefault();
            
            const target = document.querySelector(href);
            if (target) {
                target.scrollIntoView({ behavior: 'smooth' });
            }
        });
    });
}

/**
 * Close modal when clicking outside of it
 */
function initializeModalBehavior() {
    const modal = document.getElementById('downloadModal');
    
    window.addEventListener('click', function(event) {
        if (event.target === modal) {
            closeModal();
        }
    });
    
    // Close on Escape key
    document.addEventListener('keydown', function(event) {
        if (event.key === 'Escape') {
            closeModal();
        }
    });
}

/**
 * Add animation classes on scroll
 */
function initializeScrollAnimations() {
    const cards = document.querySelectorAll('.feature-card, .download-card, .support-card');
    
    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.style.animation = 'slideInUp 0.6s ease forwards';
                observer.unobserve(entry.target);
            }
        });
    }, {
        threshold: 0.1
    });
    
    cards.forEach(card => {
        observer.observe(card);
    });
}

/**
 * Add slideInUp animation to CSS dynamically
 */
function addDynamicAnimations() {
    const style = document.createElement('style');
    style.textContent = `
        @keyframes slideInUp {
            from {
                opacity: 0;
                transform: translateY(30px);
            }
            to {
                opacity: 1;
                transform: translateY(0);
            }
        }
    `;
    document.head.appendChild(style);
}

/**
 * Initialize navbar sticky behavior
 */
function initializeNavbar() {
    const navbar = document.querySelector('.navbar');
    
    window.addEventListener('scroll', () => {
        if (window.scrollY > 50) {
            navbar.style.boxShadow = '0 5px 20px rgba(0, 0, 0, 0.1)';
        } else {
            navbar.style.boxShadow = '0 2px 10px rgba(0, 0, 0, 0.05)';
        }
    });
}

/**
 * Update download link URL configuration
 * Call this function with actual download URLs
 * @param {string} platform - The platform
 * @param {string} url - The download URL
 */
function updateDownloadLink(platform, url) {
    if (downloadLinks.hasOwnProperty(platform)) {
        downloadLinks[platform] = url;
        console.log(`Updated ${platform} download link to: ${url}`);
    }
}

/**
 * Get download statistics
 * @returns {Object} Statistics object
 */
function getDownloadStats() {
    const stats = sessionStorage.getItem('downloadStats');
    return stats ? JSON.parse(stats) : {};
}

/**
 * Initialize all event listeners and behaviors on page load
 */
function initialize() {
    // Add dynamic animations
    addDynamicAnimations();
    
    // Initialize features
    initializeSmoothScroll();
    initializeModalBehavior();
    initializeScrollAnimations();
    initializeNavbar();
    
    // Fetch latest release from GitHub
    fetchLatestRelease();
    
    // Log initialization
    console.log('🚀 EZPos-System Installer Website Initialized');
}

/**
 * Run initialization when DOM is fully loaded
 */
if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', initialize);
} else {
    initialize();
}

// Export functions for use in HTML
window.downloadInstaller = downloadInstaller;
window.closeModal = closeModal;
window.scrollToDownload = scrollToDownload;
window.updateDownloadLink = updateDownloadLink;
window.getDownloadStats = getDownloadStats;
