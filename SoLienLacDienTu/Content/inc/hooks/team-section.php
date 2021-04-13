<?php
if (!function_exists('education_x_team_args')) :
    /**
     * Team Details
     *
     * @since Education X 1.0.0
     *
     * @return array $qargs team details.
     */
    function education_x_team_args()
    {
        $education_x_team_number = absint(education_x_get_option('number_of_home_team'));
        $education_x_team_from = esc_attr(education_x_get_option('select_team_from'));
        switch ($education_x_team_from) {
            case 'from-page':
                $education_x_team_page_list_array = array();
                for ($i = 1; $i <= $education_x_team_number; $i++) {
                    $education_x_team_page_list = education_x_get_option('select_page_for_team_' . $i);
                    if (!empty($education_x_team_page_list)) {
                        $education_x_team_page_list_array[] =  absint($education_x_team_page_list);
                    }
                }
                // Bail if no valid pages are selected.
                if (empty($education_x_team_page_list_array)) {
                    return;
                }
                /*page query*/
                $qargs = array(
                    'posts_per_page' => esc_attr($education_x_team_number),
                    'orderby' => 'post__in',
                    'post_type' => 'page',
                    'post__in' => $education_x_team_page_list_array,
                );
                return $qargs;
                break;

            case 'from-category':
                if (class_exists('Education_Connect') && post_type_exists('teams' )) {
                    $education_x_team_category = esc_attr(education_x_get_option('select_category_for_team'));
                    $qargs = array(
                        'post_type' => 'teams',
                        'posts_per_page' => esc_attr($education_x_team_number),
                        'tax_query' => array(
                            array(
                                'taxonomy' => 'team-category',
                                'field' => 'id',
                                'terms' => absint($education_x_team_category),
                            )
                        ));
                } else {
                    $education_x_team_category = esc_attr(education_x_get_option('select_category_for_team'));
                    $qargs = array(
                        'posts_per_page' => esc_attr($education_x_team_number),
                        'post_type' => 'post',
                        'cat' => $education_x_team_category,
                    );
                }

                return $qargs;
                break;

            default:
                break;
        }
        ?>
        <?php
    }
endif;


if (!function_exists('education_x_team')) :
    /**
     * Team
     *
     * @since Education X 1.0.0
     *
     */
    function education_x_team()
    {
        $education_x_team_excerpt_number = absint(education_x_get_option('number_of_content_home_team'));
        if (1 != education_x_get_option('show_team_section')) {
            return null;
        }
        $education_x_team_args = education_x_team_args();
        $education_x_team_query = new WP_Query($education_x_team_args); ?>
        <section class="section-block section-block-1 team-section">
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <div class="block-heading">
                            <div class="section-header">
                                <h2 class="section-title section-title2">
                                    <span><?php echo esc_html(education_x_get_option('title_team_section')); ?></span>
                                </h2>
                            </div>
                            <div class="section-content">
                                <?php echo esc_html(education_x_get_option('sub_title_team_section')); ?>
                            </div>
                        </div>
                    </div>

                    <div class="tm-team">
                        <div class="tm-team-wrapper">
                            <?php if ($education_x_team_query->have_posts()) :
                                while ($education_x_team_query->have_posts()) : $education_x_team_query->the_post();
                                    if (has_post_thumbnail()) {
                                        $thumb = wp_get_attachment_image_src(get_post_thumbnail_id(get_the_ID()), 'education-x-440-320');
                                        $url = $thumb['0'];
                                    } else {
                                        $url = '';
                                    }
                                    if (has_excerpt()) {
                                        $education_x_team_content = get_the_excerpt();
                                    } else {
                                        $education_x_team_content = education_x_words_count($education_x_team_excerpt_number, get_the_content());
                                    }
                                    ?>
                                    <div class="col-md-3 col-sm-6 col-inline">
                                        <div class="photo data-bg" data-background="<?php echo esc_url($url); ?>">
                                            <div class="overlay">
                                                <p><?php echo wp_kses_post($education_x_team_content); ?></p>
                                            </div>
                                            <div class="move">
                                                <h4 class="section-block-title">
                                                    <a href="<?php the_permalink(); ?>">
                                                        <?php the_title(); ?>
                                                    </a>
                                                </h4>
                                                <?php if (class_exists('Education_Connect') && post_type_exists('teams' )) { ?>
                                                    <div class="tm-designation"><?php education_connect_get_terms('team-designation'); ?></div>
                                                    <div class="tm-social"><?php education_connect_team_social(); ?></div>
                                                <?php } ?>
                                            </div>
                                        </div>
                                    </div>
                                <?php endwhile;
                                wp_reset_postdata();
                            endif; ?>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- End Team -->
        <?php
    }
endif;
add_action('education_x_action_front_page', 'education_x_team', 60);