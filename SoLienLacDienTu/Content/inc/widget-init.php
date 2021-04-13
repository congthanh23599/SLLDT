<?php
/**
 * Register widget area.
 *
 * @link https://developer.wordpress.org/themes/functionality/sidebars/#registering-a-sidebar
 */
function education_x_widgets_init()
{

    register_sidebar(array(
        'name' => esc_html__('Main Sidebar', 'education-x'),
        'id' => 'sidebar-1',
        'description' => esc_html__('Add widgets here.', 'education-x'),
        'before_widget' => '<div id="%1$s" class="widget %2$s">',
        'after_widget' => '</div>',
        'before_title' => '<h2 class="widget-title">',
        'after_title' => '</h2>',
    ));

    $education_x_footer_widgets_number = education_x_get_option('number_of_footer_widget');
    if ($education_x_footer_widgets_number > 0) {
        register_sidebar(array(
            'name' => esc_html__('Footer Column One', 'education-x'),
            'id' => 'footer-col-one',
            'description' => esc_html__('Displays items on footer section.', 'education-x'),
            'before_widget' => '<div id="%1$s" class="widget %2$s">',
            'after_widget' => '</div>',
            'before_title' => '<h2 class="widget-title">',
            'after_title' => '</h2>',
        ));
        if ($education_x_footer_widgets_number > 1) {
            register_sidebar(array(
                'name' => esc_html__('Footer Column Two', 'education-x'),
                'id' => 'footer-col-two',
                'description' => esc_html__('Displays items on footer section.', 'education-x'),
                'before_widget' => '<div id="%1$s" class="widget %2$s">',
                'after_widget' => '</div>',
                'before_title' => '<h2 class="widget-title">',
                'after_title' => '</h2>',
            ));
        }
        if ($education_x_footer_widgets_number > 2) {
            register_sidebar(array(
                'name' => esc_html__('Footer Column Three', 'education-x'),
                'id' => 'footer-col-three',
                'description' => esc_html__('Displays items on footer section.', 'education-x'),
                'before_widget' => '<div id="%1$s" class="widget %2$s">',
                'after_widget' => '</div>',
                'before_title' => '<h2 class="widget-title">',
                'after_title' => '</h2>',
            ));
        }
    }
}

add_action('widgets_init', 'education_x_widgets_init');
